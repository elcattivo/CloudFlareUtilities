using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloudFlareUtilities
{
    /// <summary>
    /// Provides methods to solve the JavaScript challenge, which is part of CloudFlares clearance process.
    /// </summary>
    public static class ChallengeSolver
    {
        private const string ScriptTagPattern = @"<script\b[^>]*>(?<Content>.*?)<\/script>";
        private const string ObfuscatedNumberPattern = @"(?<Number>[\(\)\+\!\[\]]+)";
        private const string SimplifiedObfuscatedDigitPattern = @"\([1+[\]]+\)";
        private const string SeedPattern = ":" + ObfuscatedNumberPattern;
        private const string OperatorPattern = @"(?<Operator>[\+\-\*\/]{1})\=";
        private const string StepPattern = OperatorPattern + ObfuscatedNumberPattern;

        /// <summary>
        /// Solves the given JavaScript challenge.
        /// </summary>
        /// <param name="challengePageContent">The HTML content of the clearance page, which contains the challenge.</param>
        /// <param name="targetHost">The hostname of the protected website.</param>
        /// <returns>The solution.</returns>
        public static ChallengeSolution Solve(string challengePageContent, string targetHost)
        {
            var jschlAnswer = DecodeSecretNumber(challengePageContent, targetHost);
            var jschlVc = Regex.Match(challengePageContent, "name=\"jschl_vc\" value=\"(?<jschl_vc>[^\"]+)").Groups["jschl_vc"].Value;
            var pass = Regex.Match(challengePageContent, "name=\"pass\" value=\"(?<pass>[^\"]+)").Groups["pass"].Value;
            var clearancePage = Regex.Match(challengePageContent, "id=\"challenge-form\" action=\"(?<action>[^\"]+)").Groups["action"].Value;            

            return new ChallengeSolution(clearancePage, jschlVc, pass, jschlAnswer);
        }

        private static int DecodeSecretNumber(string challengePageContent, string targetHost)
        {
            var challengeScript = Regex.Matches(challengePageContent, ScriptTagPattern, RegexOptions.Singleline)
                .Cast<Match>().Select(m => m.Groups["Content"].Value)
                .First(c => c.Contains("jschl-answer"));
            var seed = DeobfuscateNumber(Regex.Match(challengeScript, SeedPattern).Groups["Number"].Value);
            var steps = Regex.Matches(challengeScript, StepPattern).Cast<Match>()
                .Select(s => new Tuple<string, int>(s.Groups["Operator"].Value, DeobfuscateNumber(s.Groups["Number"].Value)));
            var secretNumber = steps.Aggregate(seed, ApplyDecodingStep) + targetHost.Length;

            return secretNumber;
        }

        private static int DeobfuscateNumber(string obfuscatedNumber)
        {
            var simplifiedObfuscatedNumber = SimplifyObfuscatedNumber(obfuscatedNumber);

            if (!simplifiedObfuscatedNumber.Contains("("))
                return CountOnes(simplifiedObfuscatedNumber);

            var digitMatches = Regex.Matches(simplifiedObfuscatedNumber, SimplifiedObfuscatedDigitPattern);
            var numberAsText = digitMatches.Cast<Match>()
                .Select(m => CountOnes(m.Value))
                .Aggregate(string.Empty, (number, digit) => number + digit);

            return int.Parse(numberAsText);
        }

        private static string SimplifyObfuscatedNumber(string obfuscatedNumber)
        {
            return obfuscatedNumber.Replace("!![]", "1").Replace("!+[]", "1");
        }

        private static int CountOnes(string obfuscatedNumber)
        {
            return obfuscatedNumber.ToCharArray().Count(c => c == '1');
        }

        private static int ApplyDecodingStep(int number, Tuple<string, int> step)
        {
            var op = step.Item1;
            var operand = step.Item2;

            switch (op)
            {
                case "+":
                    return number + operand;
                case "-":
                    return number - operand;
                case "*":
                    return number * operand;
                case "/":
                    return number / operand;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown operator: {op}");
            }
        }
    }
}