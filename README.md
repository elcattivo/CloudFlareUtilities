# CloudFlare Utilities
A .NET [PCL](https://msdn.microsoft.com/en-us/library/gg597391(v=vs.110).aspx) to bypass Cloudflare's Anti-DDoS measure (JavaScript challenge) using a DelegatingHandler.

## Example
```csharp
// Create the clearance handler.
var handler = new ClearanceHandler();

// Create a HttpClient that uses the handler.
var client = new HttpClient(handler);

// Use the HttpClient as usual. Any JS challenge will be solved automatically for you.
var content = await client.GetStringAsync("http://protected-site.tld/");
```

## Copyright & License
Copyright (c) 2016 [El Cattivo](https://github.com/elcattivo/). Code released under the [MIT license](LICENSE).
