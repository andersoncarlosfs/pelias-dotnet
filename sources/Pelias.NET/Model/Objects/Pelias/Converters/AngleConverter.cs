using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for the Angle class, facilitating JSON serialization and deserialization.
    /// </summary>
    public class AngleConverter : JsonConverter<Angle>
    {
        /// <summary>
        /// Reads and converts JSON to an Angle object.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">The serializer options.</param>
        /// <returns>An Angle object.</returns>
        public override Angle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Try to read a single (float) value from JSON
            return reader.TryGetSingle(out var degree) ? new Angle(degree) : null;
        }

        /// <summary>
        /// Writes an Angle object to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The Angle object to write.</param>
        /// <param name="options">The serializer options.</param>
        public override void Write(Utf8JsonWriter writer, Angle value, JsonSerializerOptions options)
        {
            // Write the Degrees property of the Angle object as a JSON number
            writer.WriteNumberValue(value.Degrees);
        }
    }
}
