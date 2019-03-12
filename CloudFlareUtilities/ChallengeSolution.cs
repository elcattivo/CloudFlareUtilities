using System;
using System.Globalization;

namespace CloudFlareUtilities
{
    /// <summary>
    /// Holds the information, which is required to pass the CloudFlare clearance.
    /// </summary>
    public struct ChallengeSolution : IEquatable<ChallengeSolution>
    {
        public ChallengeSolution(string clearancePage, string verificationCode, string pass, double answer, string s)
        {
            ClearancePage = clearancePage;
            S = s;
            VerificationCode = verificationCode;
            Pass = pass;
            Answer = answer;
        }

        public string ClearancePage { get; }

        public string VerificationCode { get; }

        public string Pass { get; }

        public string S { get; }

        public double Answer { get; }

        // Using .ToString("R") to reduse answer rounding
        public string ClearanceQuery => !(string.IsNullOrEmpty(S)) ?
            $"{ClearancePage}?s={Uri.EscapeDataString(S)}&jschl_vc={VerificationCode}&pass={Pass}&jschl_answer={Answer.ToString("R", CultureInfo.InvariantCulture)}" :
            $"{ClearancePage}?jschl_vc={VerificationCode}&pass={Pass}&jschl_answer={Answer.ToString("R", CultureInfo.InvariantCulture)}";

        public static bool operator ==(ChallengeSolution solutionA, ChallengeSolution solutionB)
        {
            return solutionA.Equals(solutionB);
        }

        public static bool operator !=(ChallengeSolution solutionA, ChallengeSolution solutionB)
        {
            return !(solutionA == solutionB);
        }

        public override bool Equals(object obj)
        {
            var other = obj as ChallengeSolution?;
            return other.HasValue && Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return ClearanceQuery.GetHashCode();
        }

        public bool Equals(ChallengeSolution other)
        {
            return other.ClearanceQuery == ClearanceQuery;
        }
    }
}