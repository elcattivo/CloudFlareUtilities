using Jint;
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

        private static string DecodeSecretNumber(string challengePageContent, string targetHost)
        {
            var script = Regex.Matches(challengePageContent, ScriptPattern, RegexOptions.Singleline)
                .Cast<Match>().Select(m => m.Groups["Content"].Value)
                .First(c => c.Contains("jschl-answer"));

            //var doc = new AngleSharp.Parser.Html.HtmlParser().Parse(challengePageContent);
            var contentMatch = Regex.Match(challengePageContent, @"<div.*?id=""(?<id>cf-dn.*?)""[^>]*>(?<content>.*?)</div>");
            string kContent = contentMatch.Groups["content"].Value;
            var reg = Regex.Match(script, @"(\w+?)\s?=\s?\{\s?""(\w+?)"":(.*?)\}");
            var varA = reg.Groups[1].Value;
            var varB = reg.Groups[2].Value;

            var initValue = reg.Groups[3].Value;
            var st = challengePageContent.Replace(";" + varA + "." + varB, "\r\n" + varA + "." + varB).Replace(";a.value", "\r\na.value");
            var steps = st.Split('\r', '\n').Where(x => x.StartsWith(varA + "." + varB)).Select(x => x.Replace(varA + "." + varB, "a"));

            var engine = new Engine();
            engine.SetValue("t", targetHost);
            engine.SetValue("k", "test");
            engine.Execute("var a=" + initValue + ";");
            engine.Execute("var g = String.fromCharCode;");
            engine.Execute("var  o = \"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=\";");
            engine.Execute("var document = {}; document.getElementById = function(elem){ return {innerHTML :'" + kContent + "'};};");
            engine.Execute("var e = function(s) {          s += \"==\".slice(2 - (s.length & 3));     var bm, r = \"\", r1, r2, i = 0;       for (; i < s.length;) { bm = o.indexOf(s.charAt(i++)) << 18 | o.indexOf(s.charAt(i++)) << 12 | (r1 = o.indexOf(s.charAt(i++))) << 6 | (r2 = o.indexOf(s.charAt(i++)));              r += r1 === 64 ? g(bm >> 16 & 255): r2 === 64 ? g(bm >> 16 & 255, bm >> 8 & 255): g(bm >> 16 & 255, bm >> 8 & 255, bm & 255); } return r;};");

            foreach (var step in steps)
            {
                var modStep = step.Replace("(true+\"\")[0]+\".\"+([][\"fill\"]+\"\")[3]+(+(101))[\"to\"+String[\"name\"]](21)[1]+(false+\"\")[1]+(true+\"\")[1]+Function(\"return escape\")()((\"\")[\"italics\"]())[2]+(true+[][\"fill\"])[10]+(undefined+\"\")[2]+(true+\"\")[3]+(+[]+Array)[10]+(true+\"\")[0]", "\"t.charCodeAt\"");
                modStep = modStep.Replace("function(p){var p = eval(eval(atob(\"ZG9jdW1l\")+(undefined+\"\")[1]+(true+\"\")[0]+(+(+!+[]+[+!+[]]+(!![]+[])[!+[]+!+[]+!+[]]+[!+[]+!+[]]+[+[]])+[])[+!+[]]+(false+[0]+String)[20]+(true+\"\")[3]+(true+\"\")[0]+\"Element\"+(+[]+Boolean)[10]+(NaN+[Infinity])[10]+\"Id(\"+(+(20))[\"to\"+String[\"name\"]](21)+\").\"+atob(\"aW5uZXJIVE1M\"))); return +(p)}()", kContent);
                modStep = modStep.Replace("(true+\"\")[0]+\".ch\"+(false+\"\")[1]+(true+\"\")[1]+Function(\"return escape\")()((\"\")[\"italics\"]())[2]+\"o\"+(undefined+\"\")[2]+(true+\"\")[3]+\"A\"+(true+\"\")[0]", "\"t.charCodeAt\"");
                engine.Execute(modStep);
            }
            var tmpValue = engine.Execute("a.toFixed(10)").GetCompletionValue().AsString();
            return tmpValue;
        }

        internal static string DecodeSecretNumberScript(string script)
        {
            var statements = script.Split(';');
            var stepGroups = statements.Select(GetSteps).Where(g => g.Any()).ToList();
            var steps = stepGroups.Select(ResolveStepGroup).ToList();
            var seed = steps.First().Item2;

            var secretNumber = steps.Skip(1).Aggregate(seed, ApplyDecodingStep);

            return secretNumber.ToString("#.0000000000");
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