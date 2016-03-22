using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFlareUtilities
{
    public class ClearanceHandler : DelegatingHandler
    {
        private const string CloudFlareServerName = "cloudflare-nginx";
        private const string IdCookieName = "__cfduid";
        private const string ClearanceCookieName = "cf_clearance";

        private readonly CookieContainer _cookies = new CookieContainer();
        private readonly HttpClient _client;
        private readonly HttpClientHandler _innerHandler;

        public ClearanceHandler() : this(new HttpClientHandler()) { }

        public ClearanceHandler(HttpClientHandler innerHandler) : base(innerHandler)
        {
            _innerHandler = innerHandler;
            _client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = false,
                CookieContainer = _cookies
            });
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            EnsureClientHeader(request);
            InjectCookies(request);

            var response = await base.SendAsync(request, cancellationToken);

            if (IsClearanceRequired(response))
            {
                await PassClearance(response, cancellationToken);
                InjectCookies(request);
            }

            response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        private static void EnsureClientHeader(HttpRequestMessage request)
        {
            if (!request.Headers.UserAgent.Any())
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Client", "1.0"));
        }

        private void InjectCookies(HttpRequestMessage request)
        {
            var cookies = _cookies.GetCookies(request.RequestUri).Cast<Cookie>().ToList();
            var idCookie = cookies.FirstOrDefault(c => c.Name == IdCookieName);
            var clearanceCookie = cookies.FirstOrDefault(c => c.Name == ClearanceCookieName);

            if (idCookie == null || clearanceCookie == null)
                return;

            if (_innerHandler.UseCookies)
            {
                foreach (var cookie in _innerHandler.CookieContainer.GetCookiesByName(request.RequestUri, IdCookieName, ClearanceCookieName))
                    cookie.Expired = true;

                _innerHandler.CookieContainer.SetCookies(request.RequestUri, idCookie.ToHeaderValue());
                _innerHandler.CookieContainer.SetCookies(request.RequestUri, clearanceCookie.ToHeaderValue());
            }
            else
            {
                request.Headers.Add(HttpHeader.Cookie, idCookie.ToHeaderValue());
                request.Headers.Add(HttpHeader.Cookie, clearanceCookie.ToHeaderValue());
            }
        }

        private static bool IsClearanceRequired(HttpResponseMessage response)
        {
            var isServiceUnavailable = response.StatusCode == HttpStatusCode.ServiceUnavailable;
            var isCloudFlareServer = response.Headers.Server.Any(i => i.Product.Name == CloudFlareServerName);
            var hasRedirectToClearancePage = response.Headers.Contains(HttpHeader.Refresh);

            return isServiceUnavailable && isCloudFlareServer && hasRedirectToClearancePage;
        }

        private async Task PassClearance(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            SaveIdCookie(response);

            var pageContent = await response.Content.ReadAsStringAsync();
            var scheme = response.RequestMessage.RequestUri.Scheme;
            var host = response.RequestMessage.RequestUri.Host;
            var solution = ChallengeSolver.Solve(pageContent, host);
            var clearanceUri = $"{scheme}://{host}{solution.ClearanceQuery}";

            await Task.Delay(4500, cancellationToken);

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _client.Dispose();

            base.Dispose(disposing);
        }
    }
}