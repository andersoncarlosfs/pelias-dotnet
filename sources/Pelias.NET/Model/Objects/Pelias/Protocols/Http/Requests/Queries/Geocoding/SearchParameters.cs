using Pelias.NET.Model.Objects.Pelias.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    public class SearchParameters : GeocodingBase
    {
        [JsonRequired]
        [JsonPropertyName("text")]
        [Required]
        [Text]
        public required string Text { get; set; }
    }
}
