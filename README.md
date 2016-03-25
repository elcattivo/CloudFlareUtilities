# CloudFlare Utilities
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/CloudFlareUtilities.svg)](https://www.nuget.org/packages/CloudFlareUtilities/)
[![AppVeyor](https://img.shields.io/appveyor/ci/elcattivo/CloudFlareUtilities.svg)](https://ci.appveyor.com/project/elcattivo/cloudflareutilities)
[![Codecov](https://img.shields.io/codecov/c/github/elcattivo/CloudFlareUtilities.svg)](https://codecov.io/github/elcattivo/CloudFlareUtilities)
[![GitHub license](https://img.shields.io/github/license/elcattivo/CloudFlareUtilities.svg)](https://raw.githubusercontent.com/elcattivo/CloudFlareUtilities/master/LICENSE)

A .NET [PCL](https://msdn.microsoft.com/en-us/library/gg597391(v=vs.110).aspx) to bypass Cloudflare's Anti-DDoS measure (JavaScript challenge) using a [DelegatingHandler](https://msdn.microsoft.com/en-us/library/system.net.http.delegatinghandler(v=vs.110).aspx).

## Features
- No dependencies (e.g. no external JavaScript interpreter required)
- Easily integrated into your project (implemented as DelegatingHandler; e.g. can be used with [HttpClient](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx))
- Usable on many different platforms ([PCL Profile 111](http://embed.plnkr.co/03ck2dCtnJogBKHJ9EjY/))

## Usage
```csharp
// Create the clearance handler.
var handler = new ClearanceHandler();

// Create a HttpClient that uses the handler.
var client = new HttpClient(handler);

// Use the HttpClient as usual. Any JS challenge will be solved automatically for you.
var content = await client.GetStringAsync("http://protected-site.tld/");
```
