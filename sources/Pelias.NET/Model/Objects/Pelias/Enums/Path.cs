using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Enums
{
    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Path
    {
        [EnumMember(Value = "reverse")]
        Reverse,
        [EnumMember(Value = "search")]
        Search,
        [EnumMember(Value = "search/structured")]
        StructuredSearch
    }
}
