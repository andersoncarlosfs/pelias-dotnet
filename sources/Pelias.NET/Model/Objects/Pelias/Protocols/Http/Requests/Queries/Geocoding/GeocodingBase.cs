using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    public abstract class GeocodingBase : QueryBase
    {
        [JsonPropertyName("size")]
        [AllowNull]
        public int? Size { get; set; }
    }
}
