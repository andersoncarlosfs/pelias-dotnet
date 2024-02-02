# Getting Started
This tutorial will teach the basics of an integration with the Pelias geocoding engine.

## Adding a dependency
Add a dependency in your project file using the following syntax:
```xml
<ItemGroup>
    <!-- ... -->
	<PackageReference Include="Pelias.NET" Version="1.0.0" />
    <!-- ... -->
</ItemGroup>
```

## Adding a main class
Add the following statements to a main class:
```csharp
class Program
{
	static async Task Main(string[] args)
	{
		var address = "3229 NW Pittock Dr, Portland, OR 97210, United States";

		Console.WriteLine($"Query: {address}\n");

		var client = new Client("http://localhost:4000/");

		var request = new SearchParameters() { Text = address };

		var response = await client.Search(request);

		using (StreamReader reader = new StreamReader(response))
		{
			Console.WriteLine(reader.ReadToEnd());
		}
	}
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

namespace Pelias.NET
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var address = "3229 NW Pittock Dr, Portland, OR 97210, United States";

            Console.WriteLine($"Query: {address}\n");

            var client = new Client("http://localhost:4000/");

            var request = new SearchParameters() { Text = address };

            var response = await client.Search(request);

            using (StreamReader reader = new StreamReader(response))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}
```