using Pelias.NET.Model.Interfaces.Protocols.Http;
using Pelias.NET.Model.Objects.Pelias.Converters;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Responses
{
    public class Response : IResponse<Geocoding, Feature, Properties, Geometry, BoundingBox, Coordinates, Angle>
    {
        [JsonRequired]
        [JsonPropertyName("geocoding")]
        public required Geocoding Geocoding { get; set; }
        [JsonRequired]
        [JsonPropertyName("features")]
        public required IList<Feature> Features { get; set; }
        [JsonRequired]
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonRequired]
        [JsonConverter(typeof(BoundingBoxConverter))]
        [JsonPropertyName("bbox")]
        public required BoundingBox BoundingBox { get; set; }
    }
}