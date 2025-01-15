using Pelias.NET.Model.Objects.Pelias.Extensions;
using Pelias.NET.Model.Resources;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// A custom JSON converter for serializing and deserializing <see cref="HashSet{T}"/> of enum values.
    /// This converter works for both nullable and non-nullable enums.
    /// </summary>
    /// <typeparam name="T">The type of the enum, which must be an enum type.</typeparam>
    public class EnumsConverter<T> : JsonConverter<HashSet<T>> where T : Enum
    {
        /// <summary>
        /// Reads a JSON string and converts it into a <see cref="HashSet{T}"/> of enum values.
        /// The string is expected to contain comma-separated enum names (case-insensitive).
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read the JSON data from.</param>
        /// <param name="typeToConvert">The type to convert the JSON data to.</param>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> that can be used for serialization settings.</param>
        /// <returns>A <see cref="HashSet{T}"/> containing the deserialized enum values.</returns>
        public override HashSet<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Get the type of the enum T
            var type = typeof(T);

            // Parse the comma-separated string and convert each entry into an enum value
            return reader.GetString()
                .Split(',') // Split the string by commas into individual values
                .Select(entry => entry.Trim()) // Trim any leading or trailing spaces
                .Select(entry => {
                    foreach (var property in typeof(T).GetProperties())
                    {
                        foreach(EnumMemberAttribute attribute in Attribute.GetCustomAttributes(property, typeof(EnumMemberAttribute)))
                        { 
                            if (entry == attribute.Value)
                            {
                                return (T)property.GetValue(null);
                            }
                        }
                    }

                    throw new InvalidOperationException(string.Format(ExceptionsResources.NotImplementedException_MissingEntry, entry, nameof(EnumMemberAttribute), type));
                }) // Parse each entry as an enum, case-insensitive
                .ToHashSet(); // Convert the collection into a HashSet to ensure uniqueness
        }

        /// <summary>
        /// Writes the <see cref="HashSet{T}"/> of enum values as a comma-separated string to the JSON writer.
        /// </summary>
        /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write the JSON data to.</param>
        /// <param name="value">The <see cref="HashSet{T}"/> of enum values to serialize.</param>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> that can be used for serialization settings.</param>
        public override void Write(Utf8JsonWriter writer, HashSet<T> value, JsonSerializerOptions options)
        {
            // Join the enum values into a comma-separated string and write it as lowercased values
            writer.WriteStringValue(string.Join(",", value.Select(v => v.GetEnumMemberValue()))); // Serialize the enum values as lowercase strings
        }
    }
}
