using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloudFlareUtilities
{
    /// <summary>
    /// Provides methods to solve the JavaScript challenge, which is part of CloudFlares clearance process.
    /// </summary>
    public static class ChallengeSolver
    {
        private const string IntegerSolutionTag = "parseInt(";

        private const string ScriptPattern = @"<script\b[^>]*>(?<Content>.*?)<\/script>";
        private const string ZeroPattern = @"\[\]";
        private const string OnePattern = @"\!\+\[\]|\!\!\[\]";
        private const string DigitPattern = @"\(?(\+?(" + OnePattern + @"|" + ZeroPattern + @"))+\)?";
        private const string NumberPattern = @"\+?\(?(?<Digits>\+?" + DigitPattern + @")+\)?";
        private const string OperatorPattern = @"(?<Operator>[\+|\-|\*|\/])\=?";
        private const string StepPattern = @"(" + OperatorPattern + @")??(?<Number>" + NumberPattern + ")";

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
            var s = Regex.Match(challengePageContent, "name=\"s\" value=\"(?<s>[^\"]+)").Groups["s"].Value;
            var clearancePage = Regex.Match(challengePageContent, "id=\"challenge-form\" action=\"(?<action>[^\"]+)").Groups["action"].Value;

            return new ChallengeSolution(clearancePage, jschlVc, pass, jschlAnswer, s);
        }

        private static double DecodeSecretNumber(string challengePageContent, string targetHost)
        {
            var script = Regex.Matches(challengePageContent, ScriptPattern, RegexOptions.Singleline)
                .Cast<Match>().Select(m => m.Groups["Content"].Value)
                .First(c => c.Contains("jschl-answer"));

            var statements = script.Split(';');
            var stepGroups = statements.Select(GetSteps).Where(g => g.Any()).ToList();
            var steps = stepGroups.Select(ResolveStepGroup).ToList();
            var seed = steps.First().Item2;

            var secretNumber = Math.Round(steps.Skip(1).Aggregate(seed, ApplyDecodingStep), 10) + targetHost.Length;

            return script.Contains(IntegerSolutionTag) ? (int)secretNumber : secretNumber;
        }

        private static Tuple<string, double> ResolveStepGroup(IEnumerable<Tuple<string, double>> group)
        {
            var steps = group.ToList();
            var op = steps.First().Item1;
            var seed = steps.First().Item2;

            var operand = steps.Skip(1).Aggregate(seed, ApplyDecodingStep);

            return Tuple.Create(op, operand);
        }

        private static IEnumerable<Tuple<string, double>> GetSteps(string text)
        {
            var steps = Regex.Matches(text, StepPattern).Cast<Match>()
                .Select(s => Tuple.Create(s.Groups["Operator"].Value, DeobfuscateNumber(s.Groups["Number"].Value)))
                .ToList();

            return steps;
        }

        private static double DeobfuscateNumber(string obfuscatedNumber)
        {
            var digits = Regex.Match(obfuscatedNumber, NumberPattern)
                .Groups["Digits"].Captures.Cast<Capture>()
                .Select(c => Regex.Matches(c.Value, OnePattern).Count);

            var number = double.Parse(string.Join(string.Empty, digits));

            return number;
        }

        private static double ApplyDecodingStep(double number, Tuple<string, double> step)
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