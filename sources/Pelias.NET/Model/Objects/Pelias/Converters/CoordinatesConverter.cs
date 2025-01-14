using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// A custom <see cref="JsonConverter{T}"/> for serializing and deserializing <see cref="Coordinates"/> objects.
    /// </summary>
    public class CoordinatesConverter : JsonConverter<Coordinates>
    {
        /// <summary>
        /// Recursively reads a JSON array and builds a <see cref="Coordinates"/> object.
        /// </summary>
        /// <param name="reader">The reader to read the JSON content.</param>
        /// <param name="expectedTokenType">The expected JSON token type (typically <see cref="JsonTokenType.Number"/> for coordinates).</param>
        /// <param name="angles">A list of parsed angle values (latitude and longitude).</param>
        /// <param name="limit">The maximum number of elements to parse (typically 2 for latitude and longitude).</param>
        /// <returns>A <see cref="Coordinates"/> object.</returns>
        /// <exception cref="TypeMismatchException">Thrown when the JSON token type doesn't match the expected type.</exception>
        /// <exception cref="CollectionIterationException">Thrown when the number of parsed angles exceeds the limit.</exception>
        private Coordinates GetCoordinates(ref Utf8JsonReader reader, JsonTokenType expectedTokenType, IList<double> angles, int? limit = null)
        {
            reader.Read(); // Move to the next token

            // Check if we've reached the end of the array
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                // Ensure the expected two coordinates are parsed
                if (!limit.HasValue || angles.Count == limit.Value)
                {
                    // Return a new Coordinates object created from the parsed angles
                    return new Coordinates
                    {
                        Latitude = new Angle(angles[1]),
                        Longitude = new Angle(angles[0])
                    };
                }
            }

            // Ensure the token type matches the expected one
            if ((reader.TokenType != expectedTokenType) && (!limit.HasValue || (angles.Count != limit.Value)))
            {
                throw new TypeMismatchException(
                    string.Format(
                        ExceptionsResources.TypeMismatchException_NotEqual_JsonTokenType,
                        nameof(reader),
                        reader.TokenType,
                        expectedTokenType,
                        nameof(expectedTokenType)
                    )
                );
            }

            // Check if the number of parsed angles exceeds the limit
            if (limit.HasValue && ((angles.Count >= limit.Value) || ((reader.TokenType != expectedTokenType) && (angles.Count != limit.Value))))
            {
                throw new CollectionIterationException(
                    string.Format(
                        ExceptionsResources.CollectionIterationException,
                        nameof(angles),
                        angles.Count,
                        limit.Value
                    )
                );
            }

            // Add the current token value to the angles list
            angles.Add(reader.GetDouble());

            // Continue recursively to process the next coordinate
            return GetCoordinates(ref reader, expectedTokenType, angles, limit);
        }

        /// <summary>
        /// Reads the JSON and converts it to a <see cref="Coordinates"/> object.
        /// </summary>
        /// <param name="reader">The reader to read the JSON content.</param>
        /// <param name="typeToConvert">The target type for conversion, which is <see cref="Coordinates"/> in this case.</param>
        /// <param name="options">Options for the JSON serializer.</param>
        /// <returns>A <see cref="Coordinates"/> object parsed from the JSON.</returns>
        public override Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Begin parsing the coordinates, expecting two values (latitude and longitude)
            return GetCoordinates(ref reader, JsonTokenType.Number, new List<double>(), 2);
        }

        /// <summary>
        /// Converts a <see cref="Coordinates"/> object to JSON.
        /// </summary>
        /// <param name="writer">The writer to write the JSON content.</param>
        /// <param name="value">The <see cref="Coordinates"/> object to serialize.</param>
        /// <param name="options">Options for the JSON serializer.</param>
        public override void Write(Utf8JsonWriter writer, Coordinates value, JsonSerializerOptions options)
        {
            writer.WriteStartArray(); // Begin the JSON array

            // Write each coordinate value (latitude and longitude)
            foreach (var angle in value)
            {
                writer.WriteNumberValue(angle.Degrees); // Write each coordinate as a number
            }

            writer.WriteEndArray(); // End the JSON array
        }
    }
}
