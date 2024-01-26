using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class Properties : IProperties
    {
        [JsonRequired]
        [JsonPropertyName("id")]
        public required string Identifier { get; set; }
        [JsonRequired]
        [JsonPropertyName("gid")]
        public required string GroupIdentifier { get; set; }
        [JsonRequired]
        [JsonPropertyName("layer")]
        public required string Layer { get; set; }
        [JsonRequired]
        [JsonPropertyName("source")]
        public required string Source { get; set; }
        [JsonRequired]
        [JsonPropertyName("source_id")]
        public required string SourceIdentifier { get; set; }
        [JsonRequired]
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonRequired]
        [Range(0D, 1D)]
        [JsonPropertyName("confidence")]
        public required double Confidence { get; set; }
        [JsonRequired]
        [JsonPropertyName("accuracy")]
        public required string Accuracy { get; set; }
        [JsonRequired]
        [JsonPropertyName("label")]
        public required string Label { get; set; }
        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }
        [JsonPropertyName("housenumber")]
        public string? HouseNumber { get; set; }
        [JsonPropertyName("street")]
        public string? Street { get; set; }
        [JsonPropertyName("postalcode")]
        public string? PostalCode { get; set; }
        [JsonPropertyName("match_type")]
        public Enums.MatchType? MatchType { get; set; }
        [JsonPropertyName("distance")]
        public double Distance { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        [JsonPropertyName("country_gid")]
        public string? CountryGroupIdentifier { get; set; }
        [JsonPropertyName("country_a")]
        public string? CountryAbbreviation { get; set; }
        [JsonPropertyName("macroregion")]
        public string? Macroregion { get; set; }
        [JsonPropertyName("macroregion_gid")]
        public string? MacroregionGroupIdentifier { get; set; }
        [JsonPropertyName("macroregion_a")]
        public string? MacroregionAbbreviation { get; set; }
        [JsonPropertyName("region")]
        public string? Region { get; set; }
        [JsonPropertyName("region_gid")]
        public string? RegionGroupIdentifier { get; set; }
        [JsonPropertyName("region_a")]
        public string? RegionAbbreviation { get; set; }
        [JsonPropertyName("localadmin")]
        public string? LocalAdministrator { get; set; }
        [JsonPropertyName("localadmin_gid")]
        public string? LocalAdministratorGroupIdentifier { get; set; }
        [JsonPropertyName("locality")]
        public string? Locality { get; set; }
        [JsonPropertyName("locality_gid")]
        public string? LocalityGroupIdentifier { get; set; }
        [JsonPropertyName("borough")]
        public string? Borough { get; set; }
        [JsonPropertyName("borough_gid")]
        public string? BoroughGroupIdentifier { get; set; }
        [JsonPropertyName("neighbourhood")]
        public string? Neighbourhood { get; set; }
        [JsonPropertyName("neighbourhood_gid")]
        public string? NeighbourhoodGroupIdentifier { get; set; }
        [JsonPropertyName("ocean")]
        public string? Ocean { get; set; }
        [JsonPropertyName("ocean_gid")]
        public string? OceanGroupIdentifier { get; set; }

        // Constructor with mandatory properties
        public Properties(
            string identifier,
            string groupIdentifier,
            string layer,
            string source,
            string sourceIdentifier,
            string name,
            double confidence,
            string accuracy,
            string label)
        {
            Identifier = identifier;
            GroupIdentifier = groupIdentifier;
            Layer = layer;
            Source = source;
            SourceIdentifier = sourceIdentifier;
            Name = name;
            Confidence = confidence;
            Accuracy = accuracy;
            Label = label;
        }
    }
}
