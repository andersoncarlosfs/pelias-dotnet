using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.Converters;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class Feature : IFeature<Properties, Geometry, BoundingBox, Coordinates, Angle>
    {
        [JsonRequired]
        [JsonPropertyName("type")]
        [Required]
        public required string Type { get; set; }
        [JsonRequired]
        [JsonPropertyName("properties")]
        [Required]
        public required Properties Properties { get; set; }
        [JsonRequired]
        [JsonPropertyName("geometry")]
        [Required]
        public required Geometry Geometry { get; set; }
        [JsonConverter(typeof(BoundingBoxConverter))]
        [JsonPropertyName("bbox")]
        [AllowNull]
        public BoundingBox BoundingBox { get; set; }
    }
}
