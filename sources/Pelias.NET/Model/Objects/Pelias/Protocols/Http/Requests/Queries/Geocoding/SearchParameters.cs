using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    public class SearchParameters : GeocodingBase
    {
        [JsonRequired]
        [JsonPropertyName("text")]
        public required string Text { get; set; }
    }
}
