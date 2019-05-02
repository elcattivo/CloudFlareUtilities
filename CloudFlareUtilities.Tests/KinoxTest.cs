﻿using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CloudFlareUtilities.Tests
{
    [TestClass]
    public class KinoxTest
    {

        #region constats

        private const string Host = "kinox.to";
        private const string VerificationCode1 = "1bb00fcf0ffa7618008d5d585d655e29";
        private const string Pass1 = "1458515751.766-rbEAC9yDbP";
        private const string S1 = "a33e1579d603680391f081b33f86e478ba3375bb-1550232454-1800-Ae6ohrZmyMZv%2BClUP8pj5kzeYebjJcr%2BmAKyA8WvsjXgV2v3L7FTTCpgSPxCqXZyM9VivmO4%2BPwvqnAvTnykCTQ%2B9Vde61lJZuuxA6WulIwr";
        private const string ClearancePage1 = "/cdn-cgi/l/chk_jschl";
        private const double ValidFloatAnswer1 = 179.3071197063;
        private const string Test1Html =
@"<!DOCTYPE HTML>
<html lang=""en-US"">
<head>
    <meta charset=""UTF-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge,chrome=1"" />
    <meta name=""robots"" content=""noindex, nofollow"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1"" />
    <title>Just a moment...</title>
    <script type=""text/javascript"">
        //<![CDATA[
        (function () {
            var a = function () { try { return !!window.addEventListener } catch (e) { return !1 } },
                b = function (b, c) { a() ? document.addEventListener(""DOMContentLoaded"", b, c) : document.attachEvent(""onreadystatechange"", b) };
            b(function () {
                var a = document.getElementById('cf-content'); a.style.display = 'block';
                setTimeout(function () {
                    var s, t, o, p, b, r, e, a, k, i, n, g, f, hmgtZFe = { ""PyHUZyKRg"": +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((!+[] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) };
                    g = String.fromCharCode;
                    o = ""ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="";
                    e = function (s) {
                        s += ""=="".slice(2 - (s.length & 3));
                        var bm, r = """", r1, r2, i = 0;
                        for (; i < s.length;) {
                            bm = o.indexOf(s.charAt(i++)) << 18 | o.indexOf(s.charAt(i++)) << 12
                                | (r1 = o.indexOf(s.charAt(i++))) << 6 | (r2 = o.indexOf(s.charAt(i++)));
                            r += r1 === 64 ? g(bm >> 16 & 255)
                                : r2 === 64 ? g(bm >> 16 & 255, bm >> 8 & 255)
                                    : g(bm >> 16 & 255, bm >> 8 & 255, bm & 255);
                        }
                        return r;
                    };
                    t = document.createElement('div');
                    t.innerHTML = ""<a href='/'>x</a>"";
                    t = t.firstChild.href; r = t.match(/https?:\/\//)[0];
                    t = t.substr(r.length); t = t.substr(0, t.length - 1); k = 'cf-dn-xxqKuNTrQF';
                    a = document.getElementById('jschl-answer');
                    f = document.getElementById('challenge-form');
                    ; hmgtZFe.PyHUZyKRg -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (+[]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (+[])); hmgtZFe.PyHUZyKRg *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![])) / +((+!![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![])); hmgtZFe.PyHUZyKRg -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![])) / (+(+((!+[] + !![] + !![] + !![] + !![] + []) + (+[]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![]))) + (function (p) { return eval((true + """")[0] + "".ch"" + (false + """")[1] + (true + """")[1] + Function(""return escape"")()(("""")[""italics""]())[2] + ""o"" + (undefined + """")[2] + (true + """")[3] + ""A"" + (true + """")[0] + ""("" + p + "")"") }(+((!+[] + !![] + !![] + !![] + !![] + []))))); hmgtZFe.PyHUZyKRg *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((+!![] + []) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![]) + (+!![]) + (+!![])); hmgtZFe.PyHUZyKRg *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![]) + (+!![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+!![])) / +((!+[] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![])); hmgtZFe.PyHUZyKRg *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![])); hmgtZFe.PyHUZyKRg += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])); hmgtZFe.PyHUZyKRg += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![])); hmgtZFe.PyHUZyKRg *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (+[]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + []) + (!+[] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![])); hmgtZFe.PyHUZyKRg -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((!+[] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![])); a.value = (+hmgtZFe.PyHUZyKRg).toFixed(10); '; 121'
                    f.action += location.hash;
                    f.submit();
                }, 4000);
            }, false);
        })();
      //]]>
    </script>

</head>
<body>
    <table width=""100%"" height=""100%"" cellpadding=""20"">
        <tr>
            <td align=""center"" valign=""middle"">
                <div class=""cf-browser-verification cf-im-under-attack"">
                    <noscript><h1 data-translate=""turn_on_js"" style=""color:#bd2426;"">Please turn JavaScript on and reload the page.</h1></noscript>
                    <div id=""cf-content"" style=""display:none"">
                        <div>
                            <div class=""bubbles""></div>
                            <div class=""bubbles""></div>
                            <div class=""bubbles""></div>
                        </div>
                        <h1><span data-translate=""checking_browser"">Checking your browser before accessing</span> kinox.to.</h1>
                        <a href=""https://sprengung.org/blocky.php?topic_id=0""><span style=""display: none;"">table</span></a>
                        <p data-translate=""process_is_automatic"">This process is automatic. Your browser will redirect to your requested content shortly.</p>
                        <p data-translate=""allow_5_secs"">Please allow up to 5 seconds&hellip;</p>
                    </div>

                    <form id=""challenge-form"" action=""/cdn-cgi/l/chk_jschl"" method=""get"">
                        <input type=""hidden"" name=""s"" value=""066b8c0075a754310a9ebd4ceae0753f97706789-1556697650-1800-AWWmLS7jWzbSv6YmV1lLFrOEFbrFMYlyqD9ZLGoFDjcnb2/9psSQq4bBftoZT6ZKHpwgVqJr18LKNGKeFnL3SWo9vu0OKY93OwilzQ8nzEanFmaHH1kEydx7eRVm5BUBlA==""></input>
                        <input type=""hidden"" name=""jschl_vc"" value=""7c2c676f9f6425df9acf53fef8397587"" />
                        <input type=""hidden"" name=""pass"" value=""1556697654.395-RtpNB+8kdt"" />
                        <input type=""hidden"" id=""jschl-answer"" name=""jschl_answer"" />
                    </form>
                    <div style=""display:none;visibility:hidden;"" id=""cf-dn-xxqKuNTrQF"">+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]))/+((!+[]+!![]+!![]+!![]+!![]+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(+!![])+(!+[]+!![]))</div>
                </div>
                <div class=""attribution"">
                    <a href=""https://www.cloudflare.com/5xx-error-landing?utm_source=iuam"" target=""_blank"" style=""font-size: 12px;"">DDoS protection by Cloudflare</a>
                    <br>
                    Ray ID: 4d00395afaeec2e0
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
";

        private const string Test2Html =
@"<!DOCTYPE HTML>
<html lang=""en-US"">
<head>
    <meta charset=""UTF-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge,chrome=1"" />
    <meta name=""robots"" content=""noindex, nofollow"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1"" />
    <title>Just a moment...</title>
    <script type=""text/javascript"">
        //<![CDATA[
        (function () {
            var a = function () { try { return !!window.addEventListener } catch (e) { return !1 } },
                b = function (b, c) { a() ? document.addEventListener(""DOMContentLoaded"", b, c) : document.attachEvent(""onreadystatechange"", b) };
            b(function () {
                var a = document.getElementById('cf-content'); a.style.display = 'block';
                setTimeout(function () {
                    var s, t, o, p, b, r, e, a, k, i, n, g, f, WRKYjNA = { ""lK"": +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((+!![] + []) + (!+[] + !![]) + (!+[] + !![] + !![] + !![]) + (+[]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![])) };
                    g = String.fromCharCode;
                    o = ""ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="";
                    e = function (s) {
                        s += ""=="".slice(2 - (s.length & 3));
                        var bm, r = """", r1, r2, i = 0;
                        for (; i < s.length;) {
                            bm = o.indexOf(s.charAt(i++)) << 18 | o.indexOf(s.charAt(i++)) << 12
                                | (r1 = o.indexOf(s.charAt(i++))) << 6 | (r2 = o.indexOf(s.charAt(i++)));
                            r += r1 === 64 ? g(bm >> 16 & 255)
                                : r2 === 64 ? g(bm >> 16 & 255, bm >> 8 & 255)
                                    : g(bm >> 16 & 255, bm >> 8 & 255, bm & 255);
                        }
                        return r;
                    };
                    t = document.createElement('div');
                    t.innerHTML = ""<a href='/'>x</a>"";
                    t = t.firstChild.href; r = t.match(/https?:\/\//)[0];
                    t = t.substr(r.length); t = t.substr(0, t.length - 1); k = 'cf-dn-JwLReoVgy';
                    a = document.getElementById('jschl-answer');
                    f = document.getElementById('challenge-form');
                    ; WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (+[]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![]) + (+[])); WRKYjNA.lK += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![])); WRKYjNA.lK *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((!+[] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+!![])); WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (+!![])) / +((!+[] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[])); WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / (+(+((!+[] + !![] + !![] + !![] + !![] + !![] + []) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]))) + (function (p) { return eval((true + """")[0] + "".ch"" + (false + """")[1] + (true + """")[1] + Function(""return escape"")()(("""")[""italics""]())[2] + ""o"" + (undefined + """")[2] + (true + """")[3] + ""A"" + (true + """")[0] + ""("" + p + "")"") }(+((!+[] + !![] + !![] + []))))); WRKYjNA.lK += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (+!![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![])); WRKYjNA.lK += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[])); WRKYjNA.lK += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (+!![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![])); WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + []) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![])); WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (+[])); WRKYjNA.lK *= function (p) { var p = eval(eval(e(""ZG9jdW1l"") + (undefined + """")[1] + (true + """")[0] + (+(+!+[] + [+!+[]] + (!![] + [])[!+[] + !+[] + !+[]] + [!+[] + !+[]] + [+[]]) + [])[+!+[]] + g(103) + (true + """")[3] + (true + """")[0] + ""Element"" + g(66) + (NaN + [Infinity])[10] + ""Id("" + g(107) + "")."" + e(""aW5uZXJIVE1M""))); return +(p) }(); WRKYjNA.lK += +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (+[]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![])); WRKYjNA.lK *= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (+!![]) + (!+[] + !![] + !![])); WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![])) / +((!+[] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (+[]) + (+!![])); WRKYjNA.lK -= +((!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + []) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![]) + (+[]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![]) + (!+[] + !![] + !![] + !![]) + (!+[] + !![] + !![])) / +((!+[] + !![] + !![] + !![] + !![] + !![] + []) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![]) + (+!![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![]) + (!+[] + !![] + !![] + !![] + !![] + !![] + !![] + !![] + !![]) + (!+[] + !![] + !![] + !![])); a.value = (+WRKYjNA.lK).toFixed(10); '; 121'
                    f.action += location.hash;
                    f.submit();
                }, 4000);
            }, false);
        })();
      //]]>
    </script>
</head>
<body>
    <table width=""100%"" height=""100%"" cellpadding=""20"">
        <tr>
            <td align=""center"" valign=""middle"">
                <div class=""cf-browser-verification cf-im-under-attack"">
                    <noscript><h1 data-translate=""turn_on_js"" style=""        color: #bd2426;"">Please turn JavaScript on and reload the page.</h1></noscript>
                    <div id=""cf-content"" style=""        display: none"">
                        <div style=""        display: none;""><a href=""https://sprengung.org/blocky.php?topic_id=0"">table</a></div>
                        <div>
                            <div class=""bubbles""></div>
                            <div class=""bubbles""></div>
                            <div class=""bubbles""></div>
                        </div>
                        <h1><span data-translate=""checking_browser"">Checking your browser before accessing</span> kinox.to.</h1>
                        <p data-translate=""process_is_automatic"">This process is automatic. Your browser will redirect to your requested content shortly.</p>
                        <p data-translate=""allow_5_secs"">Please allow up to 5 seconds&hellip;</p>
                    </div>
                    <form id=""challenge-form"" action=""/cdn-cgi/l/chk_jschl"" method=""get"">
                        <input type=""hidden"" name=""s"" value=""960745c925f4de55b190b0fb83d5fe905c7f92fc-1556697883-1800-AaAzcEnbhxYB5Khqf0l1uVcta88mkwb0QLDJq2iSGlGjP95zpe6/4aaZnOCLLHip/KewFsMzBKEcXfRkQmgXOK5HFbgVnW7LkrqOGQ2CWMq4OmUp/RVMMNyUPdJGs9snCQ==""></input>
                        <input type=""hidden"" name=""jschl_vc"" value=""f70f7b745449efbdd8aee7a1cc54ed18"" />
                        <input type=""hidden"" name=""pass"" value=""1556697887.775-e8mbo9F3b/"" />
                        <input type=""hidden"" id=""jschl-answer"" name=""jschl_answer"" />
                    </form>
                    <div style=""        display: none;
        visibility: hidden;"" id=""cf-dn-JwLReoVgy"">+((!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+[])+(!+[]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![])+(+!![])+(!+[]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]))/+((!+[]+!![]+[])+(!+[]+!![])+(!+[]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![])+(+[])+(!+[]+!![]+!![]+!![]+!![]+!![]+!![]+!![]+!![]))</div>
                </div>
                <div class=""attribution"">
                    <a href=""https://www.cloudflare.com/5xx-error-landing?utm_source=iuam"" target=""_blank"" style=""        font-size: 12px;"">DDoS protection by Cloudflare</a>
                    <br>
                    Ray ID: 4d003f0d8b496443
                </div>
            </td>
        </tr>
    </table>
</body>
</html>";

        private const string VerificationCode2 = "1bb00fcf0ffa7618008d5d585d655e29";
        private const string Pass2 = "1458515751.766-rbEAC9yDbP";
        private const string S2 = "a33e1579d603680391f081b33f86e478ba3375bb-1550232454-1800-Ae6ohrZmyMZv%2BClUP8pj5kzeYebjJcr%2BmAKyA8WvsjXgV2v3L7FTTCpgSPxCqXZyM9VivmO4%2BPwvqnAvTnykCTQ%2B9Vde61lJZuuxA6WulIwr";
        private const string ClearancePage2 = "/cdn-cgi/l/chk_jschl";
        private const double ValidFloatAnswer2 = 58.5639375940;

        #endregion

        [TestMethod]
        public void KinoxTest1()
        {
            var solution = ChallengeSolver.Solve(Test1Html, Host);
            Assert.AreEqual(ValidFloatAnswer1, solution.Answer);
        }

        [TestMethod]
        public void KinoxTest2()
        {
            var solution = ChallengeSolver.Solve(Test2Html, Host);
            Assert.AreEqual(ValidFloatAnswer2, solution.Answer);
        }
    }
}
