using System;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class CookieExtensionsTests
    {
        [TestMethod]
        public void ToHeaderValueReturnsHttpHeaderValue()
        {
            const string cookieName = "TestCookie";
            const string cookieValue = "Test";
            var cookie = new Cookie(cookieName, cookieValue);

            var value = cookie.ToHeaderValue();

            Assert.AreEqual($"{cookieName}={cookieValue}", value);
        }

        [TestMethod]
        public void GetCookiesByNameOnNameMismatchReturnsEmpty()
        {
            var uri = new Uri("http://domain.tld/");
            var container = new CookieContainer(1);            
            container.Add(uri, new Cookie("TestCookie", "Test"));

            var matchingCookies = container.GetCookiesByName(uri, "WrongCookie");

            Assert.IsTrue(!matchingCookies.Any());
        }

        [TestMethod]
        public void GetCookiesByNameOnDomainMismatchReturnsEmpty()
        {
            const string cookieName = "TestCookie";
            var container = new CookieContainer(1);            
            container.Add(new Uri("http://domain.tld/"), new Cookie(cookieName, "Test"));

            var matchingCookies = container.GetCookiesByName(new Uri("http://wrong-domain.tld/"), cookieName);

            Assert.IsTrue(!matchingCookies.Any());
        }

        [TestMethod]
        public void GetCookiesByNameReturnsAllMatchingCookies()
        {
            const string firstCookieName = "TestCookie1";
            const string secondCookieName = "TestCookie2";
            var uri = new Uri("http://domain.tld/");
            var container = new CookieContainer(3);
            container.Add(uri, new Cookie(firstCookieName, "Test"));
            container.Add(uri, new Cookie(secondCookieName, "Test"));
            container.Add(uri, new Cookie("WrongCookie", "Test"));

            var matchingCookies = container.GetCookiesByName(uri, firstCookieName, secondCookieName).ToList();

            Assert.IsTrue(
                matchingCookies.Count == 2 &&
                matchingCookies.Count(c => c.Name == firstCookieName) == 1 &&
                matchingCookies.Count(c => c.Name == secondCookieName) == 1);
        }
    }
}
