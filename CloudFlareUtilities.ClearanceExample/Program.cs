using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace CloudFlareUtilities.ClearanceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var target = new Uri(args[0]);
            var handler = new ClearanceHandler();
            var client = new HttpClient(handler);

            var content = client.GetStringAsync(target).Result;

            var title = Regex.Match(content, "<title>(?<Title>[^<]+)").Groups["Title"].Value.Trim();
            Console.WriteLine(title);
        }
    }
}
