using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFlareUtilities
{
    /// <summary>
    /// A HTTP handler that transparently manages Cloudflare's Anti-DDoS measure.
    /// </summary>
    /// <remarks>
    /// Only the JavaScript challenge can be handled. CAPTCHA and IP address blocking cannot be bypassed.
    /// </remarks>
    public class ClearanceHandler : DelegatingHandler
    {
        /// <summary>
        /// The default number of retries, if clearance fails.
        /// </summary>
        public static readonly int DefaultMaxRetries = 3;

        /// <summary>
        /// The default number of milliseconds to wait before sending the clearance request.
        /// </summary>
        public static readonly int DefaultClearanceDelay = 5000;

        private static readonly IEnumerable<string> CloudFlareServerNames = new[] { "cloudflare", "cloudflare-nginx" };
        private const string IdCookieName = "__cfduid";
        private const string ClearanceCookieName = "cf_clearance";
        private string StandardUserAgentIfMissing = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36";
        private string StandardAcceptHeaderIfMissing = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"; //https://developer.mozilla.org/en-US/docs/Web/HTTP/Content_negotiation/List_of_default_Accept_values

        private readonly CookieContainer _cookies = new CookieContainer();
        private readonly HttpClient _client;

        /// <summary>
        /// Creates a new instance of the <see cref="ClearanceHandler"/> class with a <see cref="HttpClientHandler"/> as inner handler.
        /// </summary>
        public ClearanceHandler() : this(new HttpClientHandler()) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ClearanceHandler"/> class with a specific inner handler.
        /// </summary>
        /// <param name="innerHandler">The inner handler which is responsible for processing the HTTP response messages.</param>
        public ClearanceHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
            _client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = false,
                CookieContainer = _cookies
            });
        }

        /// <summary>
        /// Gets or sets the number of clearance retries, if clearance fails.
        /// </summary>
        /// <remarks>A negative value causes an infinite amount of retries.</remarks>
        public int MaxRetries { get; set; } = DefaultMaxRetries;

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before sending the clearance request.
        /// </summary>
        public int ClearanceDelay { get; set; } = DefaultClearanceDelay;

        private HttpClientHandler ClientHandler => InnerHandler.GetMostInnerHandler() as HttpClientHandler;

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ClearanceHandler"/>, and optionally disposes of the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to releases only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _client.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var idCookieBefore = ClientHandler.CookieContainer.GetCookiesByName(request.RequestUri, IdCookieName).FirstOrDefault();
            var clearanceCookieBefore = ClientHandler.CookieContainer.GetCookiesByName(request.RequestUri, ClearanceCookieName).FirstOrDefault();

            if (request.Headers.UserAgent.Any())
                StandardUserAgentIfMissing = request.Headers.UserAgent.ToString();

            if (request.Headers.Accept.Any())
                StandardAcceptHeaderIfMissing = request.Headers.Accept.ToString();

            EnsureClientHeader(request.Headers, request.RequestUri);

            InjectCookies(request);

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            // (Re)try clearance if required.
            var retries = 0;
            while (IsClearanceRequired(response) && (MaxRetries < 0 || retries <= MaxRetries))
            {
                cancellationToken.ThrowIfCancellationRequested();

                await PassClearance(response, cancellationToken).ConfigureAwait(false);
                InjectCookies(request);
                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                retries++;
            }

            // Clearance failed.
            if (IsClearanceRequired(response))
                throw new CloudFlareClearanceException(retries);

            var idCookieAfter = ClientHandler.CookieContainer.GetCookiesByName(request.RequestUri, IdCookieName).FirstOrDefault();
            var clearanceCookieAfter = ClientHandler.CookieContainer.GetCookiesByName(request.RequestUri, ClearanceCookieName).FirstOrDefault();

            // inject set-cookie headers in case the cookies changed
            if (idCookieAfter != null && idCookieAfter != idCookieBefore)
            {
                response.Headers.Add(HttpHeader.SetCookie, idCookieAfter.ToHeaderValue());
            }
            if (clearanceCookieAfter != null && clearanceCookieAfter != clearanceCookieBefore)
            {
                response.Headers.Add(HttpHeader.SetCookie, clearanceCookieAfter.ToHeaderValue());
            }

            return response;
        }

        private static bool IsClearanceRequired(HttpResponseMessage response)
        {
            var isServiceUnavailable = response.StatusCode == HttpStatusCode.ServiceUnavailable;
            var isCloudFlareServer = response.Headers.Server
                .Any(i => i.Product != null && CloudFlareServerNames.Any(s => string.Compare(s, i.Product.Name, System.StringComparison.OrdinalIgnoreCase) == 0));

            return isServiceUnavailable && isCloudFlareServer;
        }

        private void InjectCookies(HttpRequestMessage request)
        {
            var cookies = _cookies.GetCookies(request.RequestUri).Cast<Cookie>().ToList();
            var idCookie = cookies.FirstOrDefault(c => c.Name == IdCookieName);
            var clearanceCookie = cookies.FirstOrDefault(c => c.Name == ClearanceCookieName);

            if (idCookie == null || clearanceCookie == null)
                return;

            if (ClientHandler.UseCookies)
            {
                ClientHandler.CookieContainer.Add(request.RequestUri, idCookie);
                ClientHandler.CookieContainer.Add(request.RequestUri, clearanceCookie);
            }
            else
            {
                request.Headers.Add(HttpHeader.Cookie, idCookie.ToHeaderValue());
                request.Headers.Add(HttpHeader.Cookie, clearanceCookie.ToHeaderValue());
            }
        }

        private void EnsureClientHeader(HttpRequestHeaders headers, System.Uri targetUri)
        {
            //The following headers are only required  for the /cdn-cgi/l/chk_jschl request | Date 05/08/19
            if (targetUri.Query.Contains("chk_jschl"))
            {
                if (headers.Host == null)
                    headers.Host = targetUri.Host;

                if (headers.Referrer == null)
                    headers.Referrer = targetUri;

                if (headers.Connection.Count == 0)
                    headers.Connection.ParseAdd("keep-alive");
            }

            //The following headers are required both for the initial request and the /cdn-cgi/l/chk_jschl request | Date 05/08/19
            if (headers.Accept.Count == 0)
                headers.Accept.ParseAdd(StandardAcceptHeaderIfMissing);

            if (headers.AcceptLanguage.Count == 0)
                headers.AcceptLanguage.ParseAdd("en,en-US;q=0.9");

            if (!headers.UserAgent.Any())
            {
                headers.UserAgent.ParseAdd(StandardUserAgentIfMissing);
            }

            if (!headers.Contains("DNT"))
                headers.Add("DNT", "1");

            if (!headers.Contains("Upgrade-Insecure-Requests"))
                headers.Add("Upgrade-Insecure-Requests", "1");
        }

        private async Task PassClearance(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            SaveIdCookie(response);

            var pageContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var solution = ChallengeSolver.Solve(pageContent, response.RequestMessage.RequestUri);

            var clearanceUri = solution.ClearanceQuery;

            await Task.Delay(ClearanceDelay, cancellationToken).ConfigureAwait(false);

            var clearanceRequest = new HttpRequestMessage(HttpMethod.Get, clearanceUri);

            EnsureClientHeader(clearanceRequest.Headers, response.RequestMessage.RequestUri);

            if (clearanceRequest.Headers.Host == null)
                clearanceRequest.Headers.Host = response.RequestMessage.RequestUri.Host;

            var passResponse = await _client.SendAsync(clearanceRequest, cancellationToken).ConfigureAwait(false);
            SaveIdCookie(passResponse); // new ID might be set as a response to the challenge in some cases
        }

        private void SaveIdCookie(HttpResponseMessage response)
        {
            var cookies = response.Headers
                .Where(pair => pair.Key == HttpHeader.SetCookie)
                .SelectMany(pair => pair.Value)
                .Where(cookie => cookie.StartsWith($"{IdCookieName}="))
                .ToList();

            if (!cookies.Any())
                return;

            // Expire any old cloudflare cookies.
            // If e.g.the cookie domain / path changed we'll have duplicate cookies (breaking the clearance) in some cases
            var oldCookies = ClientHandler.CookieContainer.GetCookies(response.RequestMessage.RequestUri);
            foreach (Cookie oldCookie in oldCookies)
            {
                if (oldCookie.Name == IdCookieName || oldCookie.Name == ClearanceCookieName)
                    oldCookie.Expired = true;
            }

            foreach (var cookie in cookies)
                _cookies.SetCookies(response.RequestMessage.RequestUri, cookie);
        }
    }
}
