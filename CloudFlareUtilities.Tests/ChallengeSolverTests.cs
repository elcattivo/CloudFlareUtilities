using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class ChallengeSolverTests
    {
        [TestMethod]
        public void SolverTest1()
        {
            var pageContent = CloudFlareUtilities.Tests.Properties.Resources.uam_zaczero_pl_test;

            var solution = ChallengeSolver.Solve(pageContent, new Uri("https://uam.zaczero.pl"));

            Assert.AreEqual("220.9846706212", solution.Jschl_answer);

            Assert.AreEqual("https://uam.zaczero.pl/cdn-cgi/l/chk_jschl?s=095491918ae160c2208b1bf4c157002b3d658ec6-1557322101-1800-AXZarxvZjnf7ImhZPUOvM9QP%2Bib5mXw2iAEijanw3i%2Fksrya7tmkvQC172UoaZC1Srk3zPVGMMcGEQtohsdCoGAS%2BoJ6JA%2BAQzJ13%2FSpqSMeRhtj2S%2BHh7EIDmjB42ePIQ%3D%3D&jschl_vc=9526100f905d05a1065d6faf43e49233&pass=1557322105.022-snbhv%2BtzRX&jschl_answer=220.9846706212", solution.ClearanceQuery);
        }

        [TestMethod]
        public void SolverTest2()
        {
            var pageContent = CloudFlareUtilities.Tests.Properties.Resources.uam_hitmehard_fun_test;

            var solution = ChallengeSolver.Solve(pageContent, new Uri("https://uam.hitmehard.fun"));

            Assert.AreEqual("238.3159095016", solution.Jschl_answer);

            Assert.AreEqual("https://uam.hitmehard.fun/cdn-cgi/l/chk_jschl?s=6a751526e16e65dbc665ab6ad7ba5f874fcc020f-1557342188-1800-AR%2BwwKhIPPW8Yq%2BTSDlnfKOjmNPCE1PtIK2nRYhuQoVcB%2B4acyAqTM6ziIW3ouNim8%2F5ltAmjquyJa8AqzI0gEkn6GALEI6mIubz99wrDyT3gWbfBf8mOJjnl8jpYDPZKg%3D%3D&jschl_vc=b11fd83e7036ce9958f69cb30c166784&pass=1557342192.927-GTjaWEbR8%2F&jschl_answer=238.3159095016", solution.ClearanceQuery);
        }
    }
}