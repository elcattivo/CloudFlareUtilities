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

        private const string CloudFlareServerName = "cloudflare-nginx";
        private const string IdCookieName = "__cfduid";
        private const string ClearanceCookieName = "cf_clearance";

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
        /// <param name="cancellationToken">A cancellation token to cancel operation.   </param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            EnsureClientHeader(request);
            InjectCookies(request);

            var response = await base.SendAsync(request, cancellationToken);

            // (Re)try clearance if required.
            var retries = 0;
            while (IsClearanceRequired(response) && (MaxRetries < 0 || retries <= MaxRetries))
            {
                cancellationToken.ThrowIfCancellationRequested();

                await PassClearance(response, cancellationToken);
                InjectCookies(request);
                response = await base.SendAsync(request, cancellationToken);

                retries++;
            }

            // Clearance failed.
            if (IsClearanceRequired(response))
                throw new CloudFlareClearanceException(retries);

            return response;
        }

        private static void EnsureClientHeader(HttpRequestMessage request)
        {
            if (!request.Headers.UserAgent.Any())
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Client", "1.0"));
        }

        private static bool IsClearanceRequired(HttpResponseMessage response)
        {
            var isServiceUnavailable = response.StatusCode == HttpStatusCode.ServiceUnavailable;
            var isCloudFlareServer = response.Headers.Server.Any(i => i.Product.Name == CloudFlareServerName);
            var isSettingIdCookie = response.Headers.Any(h => h.Key == "Set-Cookie" && h.Value.Any(v => v.Contains(IdCookieName)));

            return isServiceUnavailable && isCloudFlareServer && isSettingIdCookie;
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

        private async Task PassClearance(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            SaveIdCookie(response);

            var pageContent = await response.Content.ReadAsStringAsync();
            var scheme = response.RequestMessage.RequestUri.Scheme;
            var host = response.RequestMessage.RequestUri.Host;
            var solution = ChallengeSolver.Solve(pageContent, host);
            var clearanceUri = $"{scheme}://{host}{solution.ClearanceQuery}";

            await Task.Delay(5000, cancellationToken);

            var clearanceRequest = new HttpRequestMessage(HttpMethod.Get, clearanceUri);

            IEnumerable<string> userAgent;
            if (response.RequestMessage.Headers.TryGetValues(HttpHeader.UserAgent, out userAgent))
                clearanceRequest.Headers.Add(HttpHeader.UserAgent, userAgent);

            await _client.SendAsync(clearanceRequest, cancellationToken);
        }

        private void SaveIdCookie(HttpResponseMessage response)
        {
            var cookies = response.Headers
                .Where(pair => pair.Key == HttpHeader.SetCookie)
                .SelectMany(pair => pair.Value)
                .Where(cookie => cookie.StartsWith($"{IdCookieName}="));

            foreach (var cookie in cookies)
                _cookies.SetCookies(response.RequestMessage.RequestUri, cookie);
        }
    }
}