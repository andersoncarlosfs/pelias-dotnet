using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.Converters;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class Geometry : IGeometry<BoundingBox, Coordinates, Angle>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonConverter(typeof(CoordinatesConverter))]
        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; set; }
    }
}
