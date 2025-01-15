using Pelias.NET.Model.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries.Geocoding
{
    /// <summary>
    /// Represents the parameters for structured search in geocoding requests.
    /// Extends <see cref="GeocodingBase"/> and provides properties for address and geographic components.
    /// </summary>
    public class StructuredSearchParameters : GeocodingBase
    {
        /// <summary>
        /// Gets or sets the address to search for in the geocoding query.
        /// This field is required for the structured search.
        /// </summary>
        /// <value>
        /// A string representing the address to search for.
        /// </value>
        [JsonRequired]
        [JsonPropertyName("address")]
        [Required]
        [Text]
        public required string Address { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the neighbourhood.
        /// </value>
        [JsonPropertyName("neighbourhood")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the borough to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the borough.
        /// </value>
        [JsonPropertyName("borough")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Borough { get; set; }

        /// <summary>
        /// Gets or sets the locality to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the locality.
        /// </value>
        [JsonPropertyName("locality")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Locality { get; set; }

        /// <summary>
        /// Gets or sets the county to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the county.
        /// </value>
        [JsonPropertyName("county")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? County { get; set; }

        /// <summary>
        /// Gets or sets the region to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the region.
        /// </value>
        [JsonPropertyName("region")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the postal code.
        /// </value>
        [JsonPropertyName("postalcode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Postalcode { get; set; }

        /// <summary>
        /// Gets or sets the country to narrow down the geocoding search.
        /// This field is optional and will be ignored if set to <c>null</c>.
        /// </summary>
        /// <value>
        /// A string representing the country.
        /// </value>
        [JsonPropertyName("country")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [AllowNull]
        public string? Country { get; set; }
    }
}
