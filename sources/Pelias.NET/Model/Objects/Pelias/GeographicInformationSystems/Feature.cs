using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.Converters;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class Feature : IFeature<Properties, Geometry, BoundingBox, Coordinates, Angle>
    {
        [JsonRequired]
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonRequired]
        [JsonPropertyName("properties")]
        public required Properties Properties { get; set; }
        [JsonRequired]
        [JsonPropertyName("geometry")]
        public required Geometry Geometry { get; set; }
        [JsonRequired]
        [JsonConverter(typeof(BoundingBoxConverter))]
        [JsonPropertyName("bbox")]
        public required BoundingBox BoundingBox { get; set; }
    }
}
