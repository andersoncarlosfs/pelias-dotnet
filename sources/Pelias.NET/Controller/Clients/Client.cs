using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Interfaces;
using Pelias.NET.Model.Objects.Pelias.Extensions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Responses;
using Pelias.NET.Model.Resources;
using System.Text.Json;

namespace Pelias.NET.Controller.Services
{
    /// <summary>
    /// Service to interact with Pelias APIs, enabling geocoding and reverse geocoding operations.
    /// It provides methods for retrieving geographic data based on query parameters.
    /// </summary>
    public class Client : IClient<Response, Geocoding, Feature, Properties, Geometry, BoundingBox, Coordinates, Angle>
    {
        // API version constant
        private static readonly string Version = Model.Objects.Pelias.Enums.Version.V1.GetEnumMemberValue();

        private readonly HttpClient _client;

        /// <summary>
        /// Gets or sets the base URI for the Pelias API.
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class with a specified API endpoint and HTTP client.
        /// </summary>
        /// <param name="endpoint">The base URI of the Pelias API.</param>
        /// <param name="client">The HTTP client used for making requests.</param>
        public Client(string endpoint, HttpClient client) : this(new Uri(endpoint), client)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class with a specified API endpoint and HTTP client.
        /// </summary>
        /// <param name="endpoint">The base URI of the Pelias API.</param>
        /// <param name="client">The HTTP client used for making requests.</param>
        public Client(Uri endpoint, HttpClient client)
        {
            _client = client;
            Endpoint = endpoint;
        }

        /// <summary>
        /// Attempts to retrieve a <typeparamref name="TResponse"/> asynchronously from the Pelias API based on the specified path and query.
        /// Handles stream retrieval, deserialization, and debugging if enabled.
        /// </summary>
        /// <param name="path">The API path to retrieve data from.</param>
        /// <param name="query">The query parameters to use for the request.</param>
        /// <param name="debug">Indicates whether debug information should be included in the process.</param>
        /// <returns>The deserialized response.</returns>
        private async Task<Response?> TryGetObjectAsynchronous(Model.Objects.Pelias.Enums.Path path, QueryBase query, bool debug = false)
        {
            var exceptions = new List<Exception>();

            using (var buffer = new MemoryStream())
            {
                try
                {
                    // Retrieve the stream from the Pelias API and copy it to the buffer
                    await (await GetStreamAsynchronous(path, query)).CopyToAsync(buffer).ConfigureAwait(false);

                    // Rewind the buffer stream for deserialization
                    buffer.Position = 0;

                    // Deserialize the response into a Response object
                    var subject = await JsonSerializer.DeserializeAsync<Response>(buffer).ConfigureAwait(false);

                    if (debug)
                    {
                        // Debugging logic: Deserialize the raw JSON to compare it with the internal structure.
                        buffer.Position = 0;

                        var raw = await JsonSerializer.DeserializeAsync<JsonElement>(buffer).ConfigureAwait(false);

                        buffer.SetLength(0);
                        buffer.Position = 0;

                        await JsonSerializer.SerializeAsync(buffer, subject).ConfigureAwait(false);

                        var traversed = await JsonSerializer.DeserializeAsync<JsonElement>(buffer).ConfigureAwait(false);

                        // Collect missing properties between the raw and internal structure
                        foreach (var entry in IEntity.GetMissingProperties(raw, traversed, new List<JsonProperty>(), exceptions, false))
                        {
                            exceptions.Add(new MissingEntryException(string.Format(ExceptionsResources.MissingEntryException, entry.LastOrDefault(), $"'{{{subject?.GetType().Name}: {{{string.Join(": {", entry.Select(value => value.Name))}{new string('}', entry.Count)}}}'")));
                        }
                    }

                    if (!exceptions.Any())
                    {
                        return subject;
                    }
                }
                catch (Exception exception)
                {
                    exceptions.Add(exception);
                }

                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        /// Retrieves a stream asynchronously from the Pelias API based on the specified path and query parameters.
        /// </summary>
        /// <param name="path">The API path to retrieve data from.</param>
        /// <param name="query">The query parameters to use for the request.</param>
        /// <returns>A stream containing the API response data.</returns>
        private async Task<Stream> GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path path, QueryBase query)
        {
            // Build the URI with the specified path and query parameters
            var builder = new UriBuilder(Endpoint)
            {
                Path = $"/{Version}/{path.GetEnumMemberValue()}",
                Query = query.ToNameValueCollection().ToString()
            };

            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);

            var response = await _client.SendAsync(request);

            // Check if the response was successful and return the content stream
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }

            // If the request failed, throw an exception with details
            throw new HttpRequestException(string.Format(
                ExceptionsResources.HttpRequestException,
                request.RequestUri,
                JsonSerializer.Serialize(query),
                response.RequestMessage?.RequestUri,
                response.StatusCode,
                await response.Content.ReadAsStringAsync()
            ));
        }

        /// <summary>
        /// Retrieves a response asynchronously from the Pelias API based on the specified path and query.
        /// Uses the internal method <see cref="TryGetObjectAsynchronous"/> to handle the response.
        /// </summary>
        /// <param name="path">The API path to retrieve data from.</param>
        /// <param name="query">The query parameters to use for the request.</param>
        /// <param name="debug">Indicates whether debug information should be included.</param>
        /// <returns>The deserialized response object.</returns>
        private async Task<Response> GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path path, QueryBase query, bool debug = false)
            => await TryGetObjectAsynchronous(path, query, debug).ConfigureAwait(false);

        /// <summary>
        /// Retrieves a stream asynchronously for the reverse geocoding query.
        /// </summary>
        /// <param name="query">The query parameters for the reverse geocoding request.</param>
        /// <returns>A stream containing the reverse geocoding data.</returns>
        public async Task<Stream> Reverse(ReverseParameters query)
            => await GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path.Reverse, query);

        /// <summary>
        /// Retrieves a response asynchronously for the reverse geocoding query.
        /// </summary>
        /// <param name="query">The query parameters for the reverse geocoding request.</param>
        /// <param name="debug">Indicates whether debug information should be included.</param>
        /// <returns>The deserialized response for the reverse geocoding query.</returns>
        public async Task<Response> Reverse(ReverseParameters query, bool debug = false)
            => await GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path.Reverse, query, debug);

        /// <summary>
        /// Retrieves a stream asynchronously for the search query.
        /// </summary>
        /// <param name="query">The query parameters for the search request.</param>
        /// <returns>A stream containing the search data.</returns>
        public async Task<Stream> Search(SearchParameters query)
            => await GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path.Search, query);

        /// <summary>
        /// Retrieves a response asynchronously for the search query.
        /// </summary>
        /// <param name="query">The query parameters for the search request.</param>
        /// <param name="debug">Indicates whether debug information should be included.</param>
        /// <returns>The deserialized response for the search query.</returns>
        public async Task<Response> Search(SearchParameters query, bool debug = false)
            => await GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path.Search, query, debug);

        /// <summary>
        /// Retrieves a stream asynchronously for the structured search query.
        /// </summary>
        /// <param name="query">The query parameters for the structured search request.</param>
        /// <returns>A stream containing the structured search data.</returns>
        public async Task<Stream> Search(StructuredSearchParameters query)
            => await GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path.StructuredSearch, query);

        /// <summary>
        /// Retrieves a response asynchronously for the structured search query.
        /// </summary>
        /// <param name="query">The query parameters for the structured search request.</param>
        /// <param name="debug">Indicates whether debug information should be included.</param>
        /// <returns>The deserialized response for the structured search query.</returns>
        public async Task<Response> Search(StructuredSearchParameters query, bool debug = false)
            => await GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path.StructuredSearch, query, debug);
    }
}
