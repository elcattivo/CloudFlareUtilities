using System.Net.Http;

namespace CloudFlareUtilities
{
    internal static class HttpMessageHandlerExtensions
    {
        public static HttpMessageHandler GetMostInnerHandler(this HttpMessageHandler self)
        {
            while (true)
            {
                if (!(self is DelegatingHandler handler)) 
                    return self;

                self = handler.InnerHandler;
            }
        }
    }
}