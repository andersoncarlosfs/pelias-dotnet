using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    public class StructuredSearchParameters : GeocodingBase
    {
        [JsonRequired]
        [JsonPropertyName("address")]
        [Required]
        public required string Address { get; set; }
        [JsonPropertyName("neighbourhood")]
        [AllowNull]
        public string? Neighbourhood { get; set; }
        [JsonPropertyName("borough")]
        [AllowNull]
        public string? Borough { get; set; }
        [JsonPropertyName("locality")]
        [AllowNull]
        public string? Locality { get; set; }
        [JsonPropertyName("county")]
        [AllowNull]
        public string? County { get; set; }
        [JsonPropertyName("region")]
        [AllowNull]
        public string? Region { get; set; }
        [JsonPropertyName("postalcode")]
        [AllowNull]
        public string? Postalcode { get; set; }
        [JsonPropertyName("country")]
        [AllowNull]
        public string? Country { get; set; }
    }
}
