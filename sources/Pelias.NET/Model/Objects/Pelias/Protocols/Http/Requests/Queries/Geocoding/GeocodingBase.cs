using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    /// <summary>
    /// Represents the base class for geocoding query parameters in the Pelias API.
    /// This class contains common query properties used for geocoding requests, including controls
    /// for the response size, geographical boundaries, data layers, and sources.
    /// </summary>
    public abstract class GeocodingBase : QueryBase
    {
        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// This parameter controls the size of the response.
        /// </summary>
        /// <remarks>
        /// The value must be greater than 0. If set to <c>null</c>, no size limitation is applied.
        /// </remarks>
        [JsonPropertyName("size")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        [Range(1, int.MaxValue, ErrorMessage = "Size must be greater than 0.")]
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the layers to query for, specifying which types of geographical data to include.
        /// A comma-separated list of layer names can be provided.
        /// </summary>
        /// <remarks>
        /// The value may be <c>null</c>, indicating no specific layers are requested.
        /// If provided, it filters the results to the specified layers (e.g., "address", "neighbourhood").
        /// </remarks>
        [JsonPropertyName("layers")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Layers { get; set; }

        /// <summary>
        /// Gets or sets the sources to query from, specifying which data sources to include.
        /// A comma-separated list of source names can be provided.
        /// </summary>
        /// <remarks>
        /// The value may be <c>null</c>, indicating no specific sources are requested.
        /// If provided, it filters the results to the specified sources (e.g., "osm", "wof").
        /// </remarks>
        [JsonPropertyName("sources")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Sources { get; set; }

        /// <summary>
        /// Gets or sets the radius of the circular boundary within which the geocoding search will be performed.
        /// This is specified in meters.
        /// </summary>
        /// <remarks>
        /// The value must be a positive number. If set to <c>null</c>, no boundary is applied.
        /// </remarks>
        [JsonPropertyName("boundary.circle.radius")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        [Range(0, float.MaxValue, ErrorMessage = "Boundary radius must be a positive number.")]
        public float? BoundaryCircleRadius { get; set; }

        /// <summary>
        /// Gets or sets the country boundaries within which to limit the geocoding search.
        /// A comma-separated list of country codes (e.g., "US", "CA") can be provided.
        /// </summary>
        /// <remarks>
        /// The value may be <c>null</c>, indicating no country boundaries are specified.
        /// If provided, it restricts the search results to the specified countries.
        /// </remarks>
        [JsonPropertyName("boundary.country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? BoundaryCountries { get; set; }

        /// <summary>
        /// Gets or sets the Pelias Global Identifier (GID) for a boundary to limit the geocoding search.
        /// This specifies a particular geographical area.
        /// </summary>
        /// <remarks>
        /// The value may be <c>null</c>, indicating no specific boundary GID is provided.
        /// If provided, it restricts the search results to the specified GID.
        /// </remarks>
        [JsonPropertyName("boundary.gid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? BoundaryPeliasGlobalIdentifier { get; set; }
    }
}
