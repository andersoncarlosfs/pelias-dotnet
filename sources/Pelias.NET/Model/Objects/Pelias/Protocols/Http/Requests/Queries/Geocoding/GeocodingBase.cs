using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    /// <summary>
    /// Represents the base class for geocoding query parameters in the Pelias API.
    /// This class contains common query properties used for geocoding requests.
    /// </summary>
    public abstract class GeocodingBase : QueryBase
    {
        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// This parameter controls the size of the response.
        /// </summary>
        /// <remarks>
        /// The value must be greater than 0.
        /// </remarks>
        [JsonPropertyName("size")]
        [AllowNull]
        [Range(1, int.MaxValue)]
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the layers to query for, specifying which types of geographical data to include.
        /// A comma-separated list of layer names can be provided.
        /// </summary>
        /// <remarks>
        /// The value may be null, indicating no specific layers are requested.
        /// </remarks>
        [JsonPropertyName("layers")]
        [AllowNull]
        public string? Layers { get; set; }

        /// <summary>
        /// Gets or sets the sources to query from, specifying which data sources to include.
        /// A comma-separated list of source names can be provided.
        /// </summary>
        /// <remarks>
        /// The value may be null, indicating no specific sources are requested.
        /// </remarks>
        [JsonPropertyName("sources")]
        [AllowNull]
        public string? Sources { get; set; }

        /// <summary>
        /// Gets or sets the radius of the circular boundary within which the geocoding search will be performed.
        /// This is specified in meters.
        /// </summary>
        /// <remarks>
        /// The value must be a positive number, and it can be null to indicate no boundary is applied.
        /// </remarks>
        [JsonPropertyName("boundary.circle.radius")]
        [AllowNull]
        [Range(0, float.MaxValue)]
        public float? BoundaryCircleRadius { get; set; }

        /// <summary>
        /// Gets or sets the country boundaries within which to limit the geocoding search.
        /// A comma-separated list of country codes can be provided.
        /// </summary>
        /// <remarks>
        /// The value may be null, indicating no country boundaries are specified.
        /// </remarks>
        [JsonPropertyName("boundary.country")]
        [AllowNull]
        public string? BoundaryCountries { get; set; }

        /// <summary>
        /// Gets or sets the Pelias Global Identifier (GID) for a boundary to limit the geocoding search.
        /// This specifies a particular geographical area.
        /// </summary>
        /// <remarks>
        /// The value may be null, indicating no specific boundary GID is provided.
        /// </remarks>
        [JsonPropertyName("boundary.gid")]
        [AllowNull]
        public string? BoundaryPeliasGlobalIdentifier { get; set; }
    }
}
