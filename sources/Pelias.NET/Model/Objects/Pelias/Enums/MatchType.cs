using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Enums
{
    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MatchType
    {
        [EnumMember(Value = "exact")]
        Exact,
        [EnumMember(Value = "interpolated")]
        Interpolated,
        [EnumMember(Value = "fallback")]
        Fallback
    }
}
