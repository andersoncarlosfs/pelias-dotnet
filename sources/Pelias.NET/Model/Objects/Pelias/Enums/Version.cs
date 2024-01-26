using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Enums
{
    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Version
    {
        [EnumMember(Value = "V1")]
        V1
    }
}
