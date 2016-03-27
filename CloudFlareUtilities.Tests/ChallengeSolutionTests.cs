using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class ChallengeSolutionTests
    {
        private const string PageUrl = "/cdn-cgi/l/chk_jschl";
        private const string VerificationCode = "1bb00fcf0ffa7618008d5d585d655e29";
        private const string Pass = "1458515751.766-rbEAC9yDbP";
        private const int Answer = 0;

        [TestMethod]
        public void EqualsOnEqualSolutionReturnsTrue()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);

            var isEqual = solutionA.Equals(solutionB);

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsOnDifferentSolutionReturnsFalse()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer + 1);

            var isEqual = solutionA.Equals(solutionB);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsOnDifferentTypeReturnsFalse()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var obj = new object();

            var isEqual = solutionA.Equals(obj);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void EqualsOperatorOnEqualSolutionReturnsTrue()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);

            var isEqual = solutionA == solutionB;

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void EqualsOperatorOnDifferentSolutionReturnsFalse()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer + 1);

            var isEqual = solutionA == solutionB;

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void NotEqualsOperatorReturnsOppositeOfEqualsOperator()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);

            var isEqual = solutionA == solutionB;
            var isNotEqual = solutionA != solutionB;

            Assert.IsTrue(isEqual != isNotEqual);
        }

        [TestMethod]
        public void GetHashCodeOnEqualSolutionsReturnsSameHashCode()
        {
            var solutionA = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);
            var solutionB = new ChallengeSolution(PageUrl, VerificationCode, Pass, Answer);

            var hashCodeA = solutionA.GetHashCode();
            var hashCodeB = solutionB.GetHashCode();

            Assert.IsTrue(hashCodeA == hashCodeB);
        }
    }
}