using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class ChallengeSolutionTests
    {
        private const string PageUrl = "/cdn-cgi/l/chk_jschl";
        private const string VerificationCode = "1bb00fcf0ffa7618008d5d585d655e29";
        private const string Pass = "1458515751.766-rbEAC9yDbP";
        private const string S = "a33e1579d603680391f081b33f86e478ba3375bb-1550232454-1800-Ae6ohrZmyMZv%2BClUP8pj5kzeYebjJcr%2BmAKyA8WvsjXgV2v3L7FTTCpgSPxCqXZyM9VivmO4%2BPwvqnAvTnykCTQ%2B9Vde61lJZuuxA6WulIwr";
        private const int Answer = 0;

        [TestMethod]
        public void EqualsOnEqualSolutionReturnsTrue()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);

            var isEqual = solutionA.Equals(solutionB);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsOnDifferentSolutionReturnsFalse()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer + 1, S);

            var isEqual = solutionA.Equals(solutionB);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsOnDifferentTypeReturnsFalse()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var obj = new object();

            var isEqual = solutionA.Equals(obj);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsOperatorOnEqualSolutionReturnsTrue()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);

            var isEqual = solutionA == solutionB;

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsOperatorOnDifferentSolutionReturnsFalse()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer + 1, S);

            var isEqual = solutionA == solutionB;

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualsOperatorReturnsOppositeOfEqualsOperator()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);

            var isEqual = solutionA == solutionB;
            var isNotEqual = solutionA != solutionB;

            Assert.IsTrue(isEqual != isNotEqual);
        }

        [TestMethod]
        public void GetHashCodeOnEqualSolutionsReturnsSameHashCode()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer, S);

            var hashCodeA = solutionA.GetHashCode();
            var hashCodeB = solutionB.GetHashCode();

            Assert.IsTrue(hashCodeA == hashCodeB);
        }
    }
}