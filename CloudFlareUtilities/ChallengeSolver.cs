using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CloudFlareUtilities
{
    /// <summary>
    /// Provides methods to solve the JavaScript challenge, which is part of CloudFlares clearance process.
    /// </summary>
    public static class ChallengeSolver
    {
        /// <summary>
        /// Solves the given JavaScript challenge.
        /// </summary>
        /// <param name="challengePageContent">The HTML content of the clearance page, which contains the challenge.</param>
        /// <param name="targetUri">The Uri of the protected website.</param>
        /// <returns>The solution.</returns>
        public static ChallengeSolution Solve(string challengePageContent, Uri targetUri)
        {
            var formMatch = CloudflareRegex.JsFormRegex.Match(challengePageContent);
            var htmlHidden = CloudflareRegex.JsHtmlHiddenRegex.Match(challengePageContent);
            var scriptMatch = CloudflareRegex.ScriptRegex.Match(challengePageContent);
            var script = scriptMatch.Groups["script"].Value;
            var defineMatch = CloudflareRegex.JsDefineRegex.Match(script);
            var calcMatches = CloudflareRegex.JsCalcRegex.Matches(script);
            var resultMatch = CloudflareRegex.JsResultRegex.Match(script);
            var solveJsScript = PrepareJsScript(targetUri.Host, defineMatch, calcMatches, htmlHidden, resultMatch.Groups["addHostLength"].Success);

            var jschlAnswer = new Jint.Engine().Execute(solveJsScript).GetCompletionValue().AsString();
            var clearancepage = $"{targetUri.Scheme}://{targetUri.Host}{formMatch.Groups["action"]}";
            var s = formMatch.Groups["s"].Value;
            var jschl_vc = formMatch.Groups["jschl_vc"].Value;
            var pass = formMatch.Groups["pass"].Value;

            return new ChallengeSolution(clearancepage, jschl_vc, pass, jschlAnswer, s);
        }

        private static string PrepareJsScript(string targetHost, Match defineMatch, MatchCollection calcMatches, Match htmlHiddenMatch, bool addHostLengthToResult)
        {
            var solveScriptStringBuilder = new StringBuilder(defineMatch.Value);

            foreach (Match calcMatch in calcMatches)
            {
                if (calcMatch.Value.EndsWith("}();") && calcMatch.Value.Contains("eval(eval("))
                {
                    var i = calcMatch.Value.IndexOf("function", StringComparison.Ordinal);
                    solveScriptStringBuilder.Append(calcMatch.Value.Substring(0, i) + htmlHiddenMatch.Groups["inner"].Value + ";");
                }
                else if (calcMatch.Value.EndsWith(")))));") && calcMatch.Value.Contains("return eval("))
                {
                    var match = CloudflareRegex.JsPParamRegex.Match(calcMatch.Value);
                    if (match.Success)
                    {
                        var p = match.Groups["p"].Value;
                        var i = calcMatch.Value.IndexOf("(function", StringComparison.Ordinal);
                        solveScriptStringBuilder.Append(calcMatch.Value.Substring(0, i) + $"'{targetHost}'.charCodeAt({p})" + ");");
                    }
                }
                else
                {
                    solveScriptStringBuilder.Append(calcMatch.Value);
                }
            }

            if (addHostLengthToResult)
            {
                solveScriptStringBuilder.Append($"{defineMatch.Groups["className"].Value}.{defineMatch.Groups["propName"].Value} += {targetHost.Length};");
            }

            solveScriptStringBuilder.Append($"{defineMatch.Groups["className"].Value}.{defineMatch.Groups["propName"].Value}.toFixed(10)");

            return solveScriptStringBuilder.ToString();
        }

    }
}