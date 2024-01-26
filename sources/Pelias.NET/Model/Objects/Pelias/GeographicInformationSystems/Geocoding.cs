using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class Geocoding : IGeocoding
    {
        [JsonRequired]
        [JsonPropertyName("version")]
        public required string Version { get; set; }
        [JsonRequired]
        [JsonPropertyName("attribution")]
        public required string Attribution { get; set; }
        [JsonRequired]
        [JsonPropertyName("timestamp")]
        public required long Timestamp { get; set; }
    }
}
