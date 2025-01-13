using System.Text.Json;
using System.Text.Json.Serialization;

public class EnumCollectionConverter<T> : JsonConverter<IEnumerable<T>> where T : Enum
{
    // empty or invalid strings or nulls 
    private readonly bool _ignoreNullValues;

    // Constructor to accept the skipInvalidEnums parameter
    public EnumCollectionConverter(bool _ignoreNullValues = true)
    {
        _ignoreNullValues = _ignoreNullValues;
    }

    // ReadJson - Deserialize from a comma-separated string to the correct collection
    public override IEnumerable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? "";

        var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        // Split the string by commas and process the values
        var result = value.Split(',')
            .Select(s => s.Trim())
            .Select(s => Enum.TryParse(type, s, true, out var result) ? result : null);

        if (_ignoreNullValues || type == null) {
            result = result.Where(s => s != null);
        }

        return result.Cast<T>();
    }

    // WriteJson - Serialize the collection back to a comma-separated string
    public override void Write(Utf8JsonWriter writer, IEnumerable<T> value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(string.Join(",", value.Select(v => v?.ToString() ?? "")));
    }
}
