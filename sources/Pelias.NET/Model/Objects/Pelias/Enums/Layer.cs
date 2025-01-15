using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Enums
{
    /// <summary>
    /// Represents different layers of geographical or administrative features, typically used in geospatial applications.
    /// Each layer corresponds to a specific level or type of geographical entity, allowing for fine-grained categorization of locations.
    /// </summary>
    [Flags] // Indicates that this enum can be treated as a bit field, where each value is represented as a separate bit.
    [JsonConverter(typeof(JsonStringEnumConverter))] // Ensures the enum is serialized and deserialized as a string in JSON.
    public enum Layer
    {
        /// <summary>
        /// Points of interest such as businesses, buildings, or structures (typically entities with physical boundaries).
        /// </summary>
        [Description("Points of interest, businesses, things with walls")]
        [EnumMember(Value = "venue")]
        Venue,

        /// <summary>
        /// Locations associated with a street address (e.g., residences or office buildings).
        /// </summary>
        [Description("Places with a street address")]
        [EnumMember(Value = "address")]
        Address,

        /// <summary>
        /// Streets, roads, highways, and other thoroughfares used for transportation.
        /// </summary>
        [Description("Streets, roads, highways")]
        [EnumMember(Value = "street")]
        Street,

        /// <summary>
        /// Social communities or neighborhoods, often used to define districts or smaller local areas.
        /// </summary>
        [Description("Social communities, neighbourhoods")]
        [EnumMember(Value = "neighbourhood")]
        Neighbourhood,

        /// <summary>
        /// Local administrative boundaries, primarily referring to areas within New York City.
        /// </summary>
        [Description("A local administrative boundary, currently only used for New York City")]
        [EnumMember(Value = "borough")]
        Borough,

        /// <summary>
        /// Local administrative divisions such as municipalities, districts, or smaller jurisdictions.
        /// </summary>
        [Description("Local administrative boundaries")]
        [EnumMember(Value = "localadmin")]
        LocalAdmin,

        /// <summary>
        /// Localized areas such as towns, hamlets, or cities within a country.
        /// </summary>
        [Description("Towns, hamlets, cities")]
        [EnumMember(Value = "locality")]
        Locality,

        /// <summary>
        /// Larger official governmental areas, such as counties, that serve as primary subdivisions of a country.
        /// </summary>
        [Description("Official governmental area; usually larger than a locality but smaller than a region")]
        [EnumMember(Value = "county")]
        County,

        /// <summary>
        /// A collection of counties, typically used in European contexts to represent related administrative areas.
        /// </summary>
        [Description("A related group of counties, primarily used in Europe.")]
        [EnumMember(Value = "macrocounty")]
        MacroCounty,

        /// <summary>
        /// States or provinces, which are major subdivisions within a country.
        /// </summary>
        [Description("States and provinces")]
        [EnumMember(Value = "region")]
        Region,

        /// <summary>
        /// A collection of regions, often used to describe broader geographical areas, primarily in Europe.
        /// </summary>
        [Description("A related group of regions, mostly in Europe.")]
        [EnumMember(Value = "macroregion")]
        MacroRegion,

        /// <summary>
        /// Entire countries or nation-states, which are defined by international boundaries.
        /// </summary>
        [Description("Places that issue passports, nations, nation-states")]
        [EnumMember(Value = "country")]
        Country,

        /// <summary>
        /// A shorthand for selecting all administrative layers except for venue and address, typically used for broader location queries.
        /// </summary>
        [Description("Alias for simultaneously using all administrative layers (everything except venue and address)")]
        [EnumMember(Value = "coarse")]
        Coarse,

        /// <summary>
        /// Postal codes used by mail services to identify geographic areas for postal delivery.
        /// </summary>
        [Description("Postal code used by mail services")]
        [EnumMember(Value = "postalcode")]
        PostalCode
    }
}
