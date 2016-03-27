using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class HttpMessageHandlerExtensionsTests
    {
        [TestMethod]
        public void GetMostInnerHandlerOnNonDelegatingHandlerReturnsSameHandler()
        {
            var handler = new HttpClientHandler();

            var mostInnerHandler = handler.GetMostInnerHandler();

            Assert.AreSame(handler, mostInnerHandler);
        }

        [TestMethod]
        public void GetMostInnerHandlerOnDelegatingHandlerReturnsInnerHandler()
        {
            var innerHandler = new HttpClientHandler();
            var handler = new ClearanceHandler(innerHandler);

            var mostInnerHandler = handler.GetMostInnerHandler();

            Assert.AreSame(innerHandler, mostInnerHandler);
        }

        [TestMethod]
        public void GetMostInnerHandlerOnCascadingDelegatingHandlerReturnsMostInnerHandler()
        {
            var innerHandler = new HttpClientHandler();
            var handler = new ClearanceHandler
            {
                InnerHandler = new ClearanceHandler
                {
                    InnerHandler = innerHandler
                }
            };

            var mostInnerHandler = handler.GetMostInnerHandler();

            Assert.AreSame(innerHandler, mostInnerHandler);
        }
    }
}