# Pelias.NET
The Pelias .NET is a library written in the .NET programming language that facilitates seamless integration with the Pelias geocoding engine. It provides developers with a set of functions and methods to interact with Pelias APIs, enabling the conversion of addresses to geographic coordinates and vice versa.

## Example
```csharp
using Pelias.NET.Controller.Services;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pelias.NET
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (HttpClient http = new HttpClient())
            {
                var address = "3229 NW Pittock Dr, Portland, OR 97210, United States";
                
                Console.WriteLine($"Query: {address}\n");

                // Assuming 'pelias' is the correct Client class
                var pelias = new Client("http://localhost:4000/", http);

                // Initialize search parameters
                var request = new SearchParameters()
                {
                    Text = address,
                    Layers = new HashSet<Layer>() { Layer.Locality, Layer.Address }
                };

                // Serialize the result
                var options = new JsonSerializerOptions();
                var document = JsonSerializer.Serialize(await pelias.Search(request), options);

                Console.WriteLine(document);
            }
        }
    }
}
```
