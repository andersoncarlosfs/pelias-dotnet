using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    public class StructuredSearchParameters : GeocodingBase
    {
        [JsonRequired]
        [JsonPropertyName("address")]
        public required string Address { get; set; }
        [JsonPropertyName("neighbourhood")]
        public string? Neighbourhood { get; set; }
        [JsonPropertyName("borough")]
        public string? Borough { get; set; }
        [JsonPropertyName("locality")]
        public string? Locality { get; set; }
        [JsonPropertyName("county")]
        public string? County { get; set; }
        [JsonPropertyName("region")]
        public string? Region { get; set; }
        [JsonPropertyName("postalcode")]
        public string? Postalcode { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }
}
