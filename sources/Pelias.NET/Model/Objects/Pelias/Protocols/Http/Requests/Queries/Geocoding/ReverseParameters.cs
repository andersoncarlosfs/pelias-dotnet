using Pelias.NET.Model.Objects.Pelias.Converters;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{

    public class ReverseParameters : GeocodingBase
    {
        [JsonRequired]
        [JsonConverter(typeof(DegreesConverter))]
        [JsonPropertyName("point.lon")]
        [Required]
        public required Angle Longitude { get; set; }
        [JsonRequired]
        [JsonConverter(typeof(DegreesConverter))]
        [JsonPropertyName("point.lat")]
        [Required]
        public required Angle Latitude { get; set; }
    }
}
