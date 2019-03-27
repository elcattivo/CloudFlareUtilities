# CloudFlare Utilities
[![NuGet](https://img.shields.io/nuget/v/CloudFlareUtilities.svg)](https://www.nuget.org/packages/CloudFlareUtilities/)
[![AppVeyor](https://img.shields.io/appveyor/ci/elcattivo/CloudFlareUtilities.svg)](https://ci.appveyor.com/project/elcattivo/cloudflareutilities)
[![GitHub license](https://img.shields.io/github/license/elcattivo/CloudFlareUtilities.svg)](https://github.com/elcattivo/CloudFlareUtilities/blob/master/LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/elcattivo/CloudFlareUtilities.svg)](https://github.com/elcattivo/CloudFlareUtilities/stargazers)
[![NuGet](https://img.shields.io/nuget/dt/CloudFlareUtilities.svg)](https://www.nuget.org/packages/CloudFlareUtilities/)

A .NET Standard Library to bypass Cloudflare's Anti-DDoS measure (JavaScript challenge) using a [DelegatingHandler](https://msdn.microsoft.com/en-us/library/system.net.http.delegatinghandler(v=vs.110).aspx).

__Contributors__
- [kaso17](https://github.com/kaso17)
- [NathanNZ](https://github.com/nathannz)
- [twix20](https://github.com/twix20)
- [kade-robertson](https://github.com/kade-robertson)
- [Eric898989](https://github.com/Eric898989)
- [Grand Silence](https://github.com/grandsilence)

## Features
- No dependencies (e.g. no external JavaScript interpreter required)
- Easily integrated into your project (implemented as DelegatingHandler; e.g. can be used with [HttpClient](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx))
- Usable on many different platforms ([NET Standard 1.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.1.md))

## Usage
```csharp
try
{
    // Create the clearance handler.
    var handler = new ClearanceHandler
    {
        MaxRetries = 2 // Optionally specify the number of retries, if clearance fails (default is 3).
    };

    // Create a HttpClient that uses the handler to bypass CloudFlare's JavaScript challange.
    var client = new HttpClient(handler);

    // Use the HttpClient as usual. Any JS challenge will be solved automatically for you.
    var content = await client.GetStringAsync("http://protected-site.tld/");
}
catch (AggregateException ex) when (ex.InnerException is CloudFlareClearanceException)
{
    // After all retries, clearance still failed.
}
catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
{
    // Looks like we ran into a timeout. Too many clearance attempts?
    // Maybe you should increase client.Timeout as each attempt will take about five seconds.
}
```
