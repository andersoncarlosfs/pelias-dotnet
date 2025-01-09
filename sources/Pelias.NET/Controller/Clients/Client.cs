using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Interfaces;
using Pelias.NET.Model.Objects.Pelias.Extensions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding;
using Pelias.NET.Model.Objects.Pelias.Protocols.Http.Responses;
using Pelias.NET.Model.Resources;
using System.Net;
using System.Text.Json;

namespace Pelias.NET.Controller.Services
{
    /// <summary>
    /// A service to interact with Pelias APIs, enabling the conversion of addresses to geographic coordinates and vice versa.
    /// </summary>
    public class Client : IClient<Response, Geocoding, Feature, Properties, Geometry, BoundingBox, Coordinates, Angle>
    {
        private static readonly string Version = Model.Objects.Pelias.Enums.Version.V1.GetEnumMemberValue();

        /// <summary>
        /// Gets or sets the endpoint URI for the Pelias API.
        /// </summary>
        public Uri Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the web proxy to be used for the requests (optional).
        /// </summary>
        public WebProxy Proxy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class with the specified endpoint and an optional proxy.
        /// </summary>
        /// <param name="endpoint">The URI of the Pelias API endpoint.</param>
        /// <param name="proxy">The web proxy to be used for requests (optional).</param>
        public Client(string endpoint, WebProxy proxy = null) : this(new Uri(endpoint), proxy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class with the specified endpoint and an optional proxy.
        /// </summary>
        /// <param name="endpoint">The URI of the Pelias API endpoint.</param>
        /// <param name="proxy">The web proxy to be used for requests (optional).</param>
        public Client(Uri endpoint, WebProxy proxy = null)
        {
            Endpoint = new UriBuilder(endpoint) { Path = null, Query = null, Fragment = null }.Uri;
            Proxy = proxy;
        }

        /// <summary>
        /// Attempts to retrieve an object asynchronously from the Pelias API based on the specified path and query.
        /// </summary>
        private async Task<Response> TryGetObjectAsynchronous(Model.Objects.Pelias.Enums.Path path, QueryBase query, bool debug = false)
        {
            var exceptions = new List<Exception>();

            using (var buffer = new MemoryStream())
            {
                try
                {
                    // Retrieving the stream
                    await (await GetStreamAsynchronous(path, query)).CopyToAsync(buffer).ConfigureAwait(false);

                    // Rewinding the stream
                    buffer.Position = 0;

                    // Deserializing the response
                    var subject = await JsonSerializer.DeserializeAsync<Response>(buffer).ConfigureAwait(false);

                    if (debug)
                    {
                        // It is necessary to deserialize the object twice to get the missing properties.
                        // The objective is to reshape the object to internal format, and then compare it to the raw format (JsonElement).
                        // The source is the raw object directly deserialized from the buffer.
                        // The target is the deserialized object after a conversion to internal format. It was necessary to serialize it once to obtain a Json.

                        // Rewinding the stream
                        buffer.Position = 0;

                        // Deserializing the response as dictionary
                        var source = await JsonSerializer.DeserializeAsync<JsonElement>(buffer).ConfigureAwait(false);

                        // Clearing the stream
                        buffer.SetLength(0);
                        buffer.Position = 0;

                        await JsonSerializer.SerializeAsync<Response>(buffer, subject).ConfigureAwait(false);

                        // Deserializing the response as dictionary
                        var target = await JsonSerializer.DeserializeAsync<JsonElement>(buffer).ConfigureAwait(false);

                        // Listing the missing entries                        
                        foreach (var entry in IEntity.GetMissingProperties(source, target, new List<JsonProperty>(), exceptions, false))
                        {
                            exceptions.Add(new MissingEntryException(string.Format(ExceptionsResources.MissingEntryException, entry.LastOrDefault(), $"'{{{subject.GetType().Name}: {{{string.Join(": {", entry.Select(value => value.Name))}{new string('}', entry.Count)}}}'")));
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
        /// Creates an HTTP request for the specified path and query.
        /// </summary>
        private HttpWebRequest CreateRequest(Model.Objects.Pelias.Enums.Path path, QueryBase query)
        {
            UriBuilder builder = new UriBuilder(Endpoint)
            {
                Path = $"/{Version}/{path.GetEnumMemberValue()}",
                Query = query.ToNameValueCollection().ToString()
            };

            var request = (HttpWebRequest)WebRequest.CreateHttp(builder.Uri);

            request.Method = HttpMethod.Get.Method;
            request.Proxy = Proxy;

            return request;
        }

        /// <summary>
        /// Retrieves a stream asynchronously from the Pelias API based on the specified path and query.
        /// </summary>
        private async Task<Stream> GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path path, QueryBase query)
        {
            var request = CreateRequest(path, query);

            // Trying to get the response
            var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);

            // Getting the stream            
            if (HttpStatusCode.OK.Equals(response.StatusCode))
            {
                // Returning the content
                var content = response.GetResponseStream();

                content.ConfigureAwait(false);

                return content;
            }
            else
            {
                // Raising the exception
                throw new HttpRequestException($"The resquest to '{request.RequestUri}' with the parameters '{JsonSerializer.Serialize(query)}' returned a response from '{response.ResponseUri}' with a status code '{response.StatusCode}' and the message from the servers is '{await new StreamReader(response.GetResponseStream()).ReadToEndAsync().ConfigureAwait(false)}'.");
            }
        }


        /// <summary>
        /// Retrieves a response asynchronously from the Pelias API based on the specified path and query.
        /// </summary>
        private async Task<Response> GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path path, QueryBase query, bool debug = false)
            => await TryGetObjectAsynchronous(path, query, debug).ConfigureAwait(false);

        /// <summary>
        /// Retrieves a stream asynchronously for the reverse geocoding query.
        /// </summary>
        public async Task<Stream> Reverse(ReverseParameters query)
            => await GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path.Reverse, query);

        /// <summary>
        /// Retrieves a response asynchronously for the reverse geocoding query.
        /// </summary>
        public async Task<Response> Reverse(ReverseParameters query, bool debug = false)
            => await GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path.Reverse, query, debug);

        /// <summary>
        /// Retrieves a stream asynchronously for the search query.
        /// </summary>
        public async Task<Stream> Search(SearchParameters query)
            => await GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path.Search, query);

        /// <summary>
        /// Retrieves a response asynchronously for the search query.
        /// </summary>
        public async Task<Response> Search(SearchParameters query, bool debug = false)
            => await GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path.Search, query, debug);

        /// <summary>
        /// Retrieves a stream asynchronously for the structured search query.
        /// </summary>
        public async Task<Stream> Search(StructuredSearchParameters query)
            => await GetStreamAsynchronous(Model.Objects.Pelias.Enums.Path.StructuredSearch, query);

        /// <summary>
        /// Retrieves a response asynchronously for the structured search query.
        /// </summary>
        public async Task<Response> Search(StructuredSearchParameters query, bool debug = false)
            => await GetResponseAsynchronous(Model.Objects.Pelias.Enums.Path.StructuredSearch, query, debug);
    }
}
