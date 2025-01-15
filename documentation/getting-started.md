# Getting Started
This tutorial will teach the basics of an integration with the Pelias geocoding engine.

## Adding a dependency
Add a dependency in your project file using the following syntax:
```xml
<ItemGroup>
    <!-- ... -->
	<PackageReference Include="Pelias.NET" Version="2.0.0" />
    <!-- ... -->
</ItemGroup>
```

## Adding a main class
Add the following statements to a main class:
```csharp
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
```

In order to be able to reference Pelias.NET, it is necessary to add the following using statement at the top of the file:
```csharp
using Pelias.NET.Controller.Services;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding;
```

The complete file is shown below:
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
