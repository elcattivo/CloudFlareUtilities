using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class ChallengeSolverTests
    {
        private const string Host = "domain.tld";
        private const string VerificationCode = "1bb00fcf0ffa7618008d5d585d655e29";
        private const string Pass = "1458515751.766-rbEAC9yDbP";
        private const string S = "a33e1579d603680391f081b33f86e478ba3375bb-1550232454-1800-Ae6ohrZmyMZv%2BClUP8pj5kzeYebjJcr%2BmAKyA8WvsjXgV2v3L7FTTCpgSPxCqXZyM9VivmO4%2BPwvqnAvTnykCTQ%2B9Vde61lJZuuxA6WulIwr";
        private const string ClearancePage = "/cdn-cgi/l/chk_jschl";
        private const int ValidIntegerAnswer = 293;
        private const double ValidFloatAnswer = 47.1687814926;

        private const string IntegerChallengeScript =
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

        private const string FloatChallengeScript =
@"<script type=""text/javascript"">
  //<![CDATA[
  (function(){
    var a = function() {try{return !!window.addEventListener} catch(e) {return !1} },
    b = function(b, c) {a() ? document.addEventListener(""DOMContentLoaded"", b, c) : document.attachEvent(""onreadystatechange"", b)};
    b(function(){
      var a = document.getElementById('cf-content');a.style.display = 'block';
      setTimeout(function(){
        var s,t,o,p,b,r,e,a,k,i,n,g,f, bnuYckT={""VcNScacY"":+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]))/+((!+[]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(+!![])+(+[])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[]))};
        t = document.createElement('div');
        t.innerHTML=""<a href='/'>x</a>"";
        t = t.firstChild.href;r = t.match(/https?:\/\//)[0];
        t = t.substr(r.length); t = t.substr(0,t.length-1);
        a = document.getElementById('jschl-answer');
        f = document.getElementById('challenge-form');
        ;bnuYckT.VcNScacY*=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]))/+((!+[]+!![]+[])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(+[])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]));bnuYckT.VcNScacY+=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]))/+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(+[]));bnuYckT.VcNScacY*=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]))/+((+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]));bnuYckT.VcNScacY*=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]))/+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]));bnuYckT.VcNScacY*=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]))/+((!+[]+!![]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]));bnuYckT.VcNScacY-=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]))/+((!+[]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]));bnuYckT.VcNScacY-=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]))/+((+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]));bnuYckT.VcNScacY+=+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]))/+((!+[]+!![]+[])+(!+[]+!![])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]));a.value = +bnuYckT.VcNScacY.toFixed(10) + t.length; '; 121'
        f.action += location.hash;
        f.submit();
      }, 4000);
    }, false);
  })();
  //]]>
</script>";

        private static readonly string Form =
$@"<form id=""challenge-form"" action=""{ClearancePage}"" method=""get"">
    <input type=""hidden"" name=""jschl_vc"" value=""{VerificationCode}""/>
    <input type=""hidden"" name=""pass"" value=""{Pass}""/>
<input type=""hidden"" name=""s"" value=""{S}""/>
    <input type=""hidden"" id=""jschl-answer"" name=""jschl_answer""/>
</form>";

        [TestMethod]
        public void SolveReturnsValidIntegerAnswer()
        {
            var pageContent = IntegerChallengeScript + Form;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(ValidIntegerAnswer, solution.Answer);
        }

        [TestMethod]
        public void SolveReturnsValidFloatAnswer()
        {
            var pageContent = FloatChallengeScript + Form;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(ValidFloatAnswer, solution.Answer);
        }

        [TestMethod]
        public void SolveReturnsVerificationCode()
        {
            var pageContent = IntegerChallengeScript + Form;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(VerificationCode, solution.VerificationCode);
        }

        [TestMethod]
        public void SolveReturnsPass()
        {
            var pageContent = IntegerChallengeScript + Form;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(Pass, solution.Pass);
        }

        [TestMethod]
        public void SolveReturnsClearancePage()
        {
            var pageContent = IntegerChallengeScript + Form;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(ClearancePage, solution.ClearancePage);
        }

        [TestMethod]
        public void SolveReturnsValidClearanceQuery()
        {
            var pageContent = IntegerChallengeScript + Form;

            var solution = ChallengeSolver.Solve(pageContent, Host);

            Assert.AreEqual(
                $"{ClearancePage}?s={S}&jschl_vc={VerificationCode}&pass={Pass}&jschl_answer={ValidIntegerAnswer}",
                solution.ClearanceQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ApplyDecodingStepOnUnknownOperatorThrowsArgumentOutOfRangeException()
        {
            const string invalidOperator = "%";
            try
            {
                var solverType = new PrivateType(typeof(ChallengeSolver));
                solverType.InvokeStatic("ApplyDecodingStep", 323D, Tuple.Create(invalidOperator, 32D));
            }
            //We're calling this with reflection, so we have to throw up the TargetInvocationException to represent what would actually occur.
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}