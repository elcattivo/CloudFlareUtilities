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
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36 OPR/54.0.2952.60");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Referer", target.AbsoluteUri);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");

            try
            {
                var content = client.GetStringAsync(target).Result;

                var title = Regex.Match(content, "<title>(?<Title>[^<]+)").Groups["Title"].Value.Trim();
                Console.WriteLine(title);
            }
            catch (AggregateException ex) when (ex.InnerException is CloudFlareClearanceException)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
