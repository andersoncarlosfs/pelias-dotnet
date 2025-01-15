# Pelias.NET
The Pelias .NET is a library written in the .NET programming language that facilitates seamless integration with the Pelias geocoding engine. It provides developers with a set of functions and methods to interact with Pelias APIs, enabling the conversion of addresses to geographic coordinates and vice versa.

## Example
```csharp
using Pelias.NET.Controller.Services;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding;

namespace Pelias.NET
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var address = "3229 NW Pittock Dr, Portland, OR 97210, United States";

            Console.WriteLine($"Query: {address}\n");

            var client = new Client("http://localhost:4000/");

            var request = new SearchParameters() { Text = address, Layers = new HashSet<Layer>() { Layer.Locality, Layer.Address } };

            var options = new JsonSerializerOptions();

            var document = JsonSerializer.Serialize(await client.Search(request), options);

            Console.WriteLine(document);
        }
    }
}
```
