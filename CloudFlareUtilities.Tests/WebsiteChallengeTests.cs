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
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36");

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
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36");

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


        
    }
}
