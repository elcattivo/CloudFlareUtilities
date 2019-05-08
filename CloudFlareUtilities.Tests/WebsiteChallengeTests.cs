using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class WebsiteChallengeTests
    {
        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun()
        {
            var handler = new ClearanceHandler();
            var client = new HttpClient(handler);

            try
            {
                var content = client.GetStringAsync(new Uri("https://uam.hitmehard.fun/HIT")).Result;
            }
            catch (AggregateException ae) when (!(ae.InnerException is HttpRequestException))
            {
                Assert.Fail(ae.InnerException.Message);
            }
            catch (AggregateException ae) when (ae.InnerException is HttpRequestException)
            {
                Assert.IsTrue(ae.InnerException.Message.IndexOf("404 (Not Found)", StringComparison.OrdinalIgnoreCase) >= 0, ae.InnerException.Message);
            }
           
        }


        [TestMethod]
        public void SolveWebsiteChallenge_uamzaczeropl()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var handler = new ClearanceHandler();
            var client = new HttpClient(handler);

            try
            {
                var content = client.GetStringAsync(new Uri("https://uam.zaczero.pl/")).Result;
                Assert.IsTrue(content.IndexOf("ok", StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch (Exception ex) 
            {
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod]
        public void SolveWebsiteChallenge_uamzaczeropl_withdefaultuseragent()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var handler = new ClearanceHandler();
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (iPhone; CPU iPhone OS 12_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1");
            try
            {
                var content = client.GetStringAsync(new Uri("https://uam.zaczero.pl/")).Result;
                Assert.IsTrue(content.IndexOf("ok", StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }


        [TestMethod]
        public void SolveWebsiteChallenge_lionroyalcpkclub_CustomPort()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var handler = new ClearanceHandler();
            var client = new HttpClient(handler);
            try
            {
                var content = client.GetStringAsync(new Uri("http://lionroyalcpk.club:2082/")).Result;

                Assert.IsTrue(content.IndexOf("Lion Royal Casino", StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
