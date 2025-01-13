using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YourNamespace
{
    /// <summary>
    /// A custom JSON converter that serializes and deserializes collections of enums as a delimited string.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    public class EnumCollectionJsonConverter<T> : JsonConverter<IEnumerable<T>> where T : Enum
    {
        // The separator character used to join the enum values in the string (default is comma).
        private readonly char _separator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumCollectionJsonConverter{T}"/> class.
        /// </summary>
        /// <param name="separator">The character used to separate enum values in the serialized string. Defaults to a comma.</param>
        public EnumCollectionJsonConverter(char separator = ',')
        {
            _separator = separator;
        }

        /// <summary>
        /// Reads a JSON string and converts it into a collection of enum values.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The target type to convert to.</param>
        /// <param name="options">The serializer options.</param>
        /// <returns>A collection of enum values.</returns>
        public override IEnumerable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read the delimited string from the JSON input.
            string input = reader.GetString();

            // Split the input string by the separator, trim the individual values,
            // and convert each value to the corresponding enum type. 
            // Invalid values are ignored.
            return input.Split(_separator)
                        .Select(value => Enum.TryParse<T>(value.Trim(), ignoreCase: true, out var result) ? result : (T?)null)
                        .Where(value => value.HasValue)
                        .Cast<T>()
                        .ToArray();
        }

        /// <summary>
        /// Writes a collection of enum values as a delimited string to the JSON output.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The collection of enum values to serialize.</param>
        /// <param name="options">The serializer options.</param>
        public override void Write(Utf8JsonWriter writer, IEnumerable<T> value, JsonSerializerOptions options)
        {
            // Convert the enum values to lowercase strings and join them using the separator.
            string joined = string.Join(_separator.ToString(), value.Select(v => v.ToString().ToLower()));

            // Write the resulting string to the JSON output.
            writer.WriteStringValue(joined);
        }
    }
}
