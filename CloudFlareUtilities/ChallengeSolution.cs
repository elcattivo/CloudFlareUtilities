using System;
using System.Globalization;

namespace CloudFlareUtilities
{
    /// <summary>
    /// Holds the information, which is required to pass the CloudFlare clearance.
    /// </summary>
    public struct ChallengeSolution : IEquatable<ChallengeSolution>
    {
        public ChallengeSolution(string clearancePage, string jschl_vc, string pass, string jschl_answer, string s)
        {
            ClearancePage = clearancePage;
            S = s;
            VerificationCode = jschl_vc;
            Pass = pass;
            Jschl_answer = jschl_answer;
            ClearanceQuery = clearancePage + $"?s={Uri.EscapeDataString(s)}" +
                        $"&jschl_vc={Uri.EscapeDataString(VerificationCode)}" +
                        $"&pass={Uri.EscapeDataString(pass)}" +
                        $"&jschl_answer={Uri.EscapeDataString(jschl_answer)}";
        }

        public string ClearancePage { get; }

        public string VerificationCode { get; }

        public string Pass { get; }

        public string S { get; }

        public string Jschl_answer { get; }

        public string ClearanceQuery { get; }
            

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