using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom <see cref="JsonConverter{T}"/> for converting a <see cref="BoundingBox"/> to and from JSON.
    /// </summary>
    public class BoundingBoxConverter : JsonConverter<BoundingBox>
    {
        /// <summary>
        /// Reads JSON and converts it into a <see cref="BoundingBox"/> object.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> instance used to read the JSON data.</param>
        /// <param name="typeToConvert">The type to convert to (expected to be <see cref="BoundingBox"/>).</param>
        /// <param name="options">Additional options that may control the serialization/deserialization process.</param>
        /// <returns>A <see cref="BoundingBox"/> instance representing the deserialized JSON data.</returns>
        public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Start the recursive parsing process and return the resulting BoundingBox
            return GetBoundingBox(ref reader, JsonTokenType.Number, new List<double>(), 4);
        }

        /// <summary>
        /// Recursively reads and builds a <see cref="BoundingBox"/> object from JSON.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> instance used to read the JSON data.</param>
        /// <param name="expectedTokenType">The expected token type for the values in the bounding box (typically <see cref="JsonTokenType.Number"/>).</param>
        /// <param name="angles">A list of angles (coordinates) being parsed, initially empty.</param>
        /// <param name="limit">An optional limit to restrict the number of coordinates (usually 4 for bounding boxes).</param>
        /// <returns>A <see cref="BoundingBox"/> instance built from the parsed values.</returns>
        /// <exception cref="TypeMismatchException">Thrown if the expected token type does not match the actual token type.</exception>
        /// <exception cref="CollectionIterationException">Thrown if the number of coordinates exceeds the specified limit.</exception>
        private BoundingBox GetBoundingBox(ref Utf8JsonReader reader, JsonTokenType expectedTokenType, IList<double> angles, int? limit)
        {
            // Read the next token
            reader.Read();

            // Get the current token type
            var currentTokenType = reader.TokenType;

            // If we have reached the end of the array, build the BoundingBox object
            if (currentTokenType == JsonTokenType.EndArray)
            {
                // Ensure the expected four coordinates are parsed
                if (angles.Count != 4)
                {
                    throw new TypeMismatchException(string.Format(ExceptionsResources.TypeMismatchException_InvalidBoundingBoxCoordinates, angles.Count));
                }

                return new BoundingBox
                {
                    TopRightCoordinates = new Coordinates
                    {
                        Latitude = new Angle(angles[1]),
                        Longitude = new Angle(angles[0])
                    },
                    BottomLeftCoordinates = new Coordinates
                    {
                        Latitude = new Angle(angles[3]),
                        Longitude = new Angle(angles[2])
                    }
                };
            }

            // Ensure that the token is the expected type
            if (currentTokenType != expectedTokenType)
            {
                throw new TypeMismatchException(string.Format(ExceptionsResources.TypeMismatchException_NotEqual_JsonTokenType, nameof(reader), currentTokenType, expectedTokenType, nameof(expectedTokenType)));
            }

            // If a limit is set, ensure that the number of coordinates does not exceed it
            if (limit.HasValue && angles.Count >= limit)
            {
                throw new CollectionIterationException(string.Format(ExceptionsResources.CollectionIterationException, nameof(angles), angles.Count, limit));
            }

            // Add the current value (coordinate) to the angles list
            angles.Add(reader.GetDouble());

            // Continue the recursive process
            return GetBoundingBox(ref reader, expectedTokenType, angles, limit);
        }

        /// <summary>
        /// Writes a <see cref="BoundingBox"/> object to JSON.
        /// </summary>
        /// <param name="writer">The <see cref="Utf8JsonWriter"/> instance used to write the JSON data.</param>
        /// <param name="value">The <see cref="BoundingBox"/> instance to serialize to JSON.</param>
        /// <param name="options">Additional options that may control the serialization process.</param>
        public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            // Start writing the JSON array for the bounding box coordinates
            writer.WriteStartArray();

            // Write each coordinate of the bounding box
            foreach (var entry in value.ToArray())
            {
                writer.WriteNumberValue(entry);
            }

            // End the JSON array
            writer.WriteEndArray();
        }
    }
}
