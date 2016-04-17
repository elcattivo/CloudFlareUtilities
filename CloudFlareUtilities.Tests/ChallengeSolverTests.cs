using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class ChallengeSolverTests
    {
        private const string Host = "domain.tld";
        private const string VerificationCode = "1bb00fcf0ffa7618008d5d585d655e29";
        private const string Pass = "1458515751.766-rbEAC9yDbP";
        private const string ClearancePage = "/cdn-cgi/l/chk_jschl";
        private const int ValidAnswer = 293;

        private const string JavaScriptData =
@"<script type=""text/javascript"">
  //<![CDATA[
  (function(){
    var a = function() {try{return !!window.addEventListener} catch(e) {return !1} },
    b = function(b, c) {a() ? document.addEventListener(""DOMContentLoaded"", b, c) : document.attachEvent(""onreadystatechange"", b)};
    b(function(){
      var a = document.getElementById('cf-content');a.style.display = 'block';
      setTimeout(function(){
        var t,r,a,f, BAaNzsI={""BthP"":+((!+[]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]))};
        t = document.createElement('div');
        t.innerHTML=""<a href='/'>x</a>"";
        t = t.firstChild.href;r = t.match(/https?:\\/\\//)[0];
        t = t.substr(r.length); t = t.substr(0,t.length-1);
        a = document.getElementById('jschl-answer');
        f = document.getElementById('challenge-form');
        ;BAaNzsI.BthP-=+((+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]));BAaNzsI.BthP+=!+[]+!![];BAaNzsI.BthP*=+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]));BAaNzsI.BthP/=!+[]+!![];a.value = parseInt(BAaNzsI.BthP, 10) + t.length;
        f.submit();
      }, 4000);
    }, false);
  })();
  //]]>
</script>";

        private static readonly string FormData =
$@"<form id=""challenge-form"" action=""{ClearancePage}"" method=""get"">
    <input type=""hidden"" name=""jschl_vc"" value=""{VerificationCode}""/>
    <input type=""hidden"" name=""pass"" value=""{Pass}""/>
    <input type=""hidden"" id=""jschl-answer"" name=""jschl_answer""/>
</form>";

        [TestMethod]
        public void SolveReturnsValidAnswer()
        {
            var pageContent = JavaScriptData + FormData;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(ValidAnswer, solution.Answer);
        }

        [TestMethod]
        public void SolveReturnsVerificationCode()
        {
            var pageContent = JavaScriptData + FormData;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(VerificationCode, solution.VerificationCode);
        }

        [TestMethod]
        public void SolveReturnsPass()
        {
            var pageContent = JavaScriptData + FormData;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(Pass, solution.Pass);
        }

        [TestMethod]
        public void SolveReturnsClearancePage()
        {
            var pageContent = JavaScriptData + FormData;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(ClearancePage, solution.ClearancePage);
        }

        [TestMethod]
        public void SolveReturnsValidClearanceQuery()
        {
            var pageContent = JavaScriptData + FormData;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(
                $"{ClearancePage}?jschl_vc={VerificationCode}&pass={Pass}&jschl_answer={ValidAnswer}",
                solution.ClearanceQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ApplyDecodingStepOnUnknownOperatorThrowsArgumentOutOfRangeException()
        {
            const string invalidOperator = "%";
            var solverType = new PrivateType(typeof (ChallengeSolver));
            solverType.InvokeStatic("ApplyDecodingStep", 323, Tuple.Create(invalidOperator, 32));
        }
    }
}