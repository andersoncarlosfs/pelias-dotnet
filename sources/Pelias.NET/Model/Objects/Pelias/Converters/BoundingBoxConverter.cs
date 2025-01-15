using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for <see cref="BoundingBox"/>, handling the serialization and deserialization of a bounding box.
    /// </summary>
    public class BoundingBoxConverter : JsonConverter<BoundingBox>
    {
        private static readonly int _length = new BoundingBox() { TopRightCoordinates = new Coordinates(), BottomLeftCoordinates = new Coordinates() }.SelectMany(coordinates => coordinates).Count();

        /// <summary>
        /// Reads a JSON value and converts it into a <see cref="BoundingBox"/> object.
        /// </summary>
        /// <param name="reader">The JSON reader to read the value from.</param>
        /// <param name="typeToConvert">The target type to convert to (<see cref="BoundingBox"/>).</param>
        /// <param name="options">The serializer options to use for deserialization.</param>
        /// <returns>A <see cref="BoundingBox"/> object representing the deserialized bounding box.</returns>
        public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var angles = new List<Angle>();

            // Read the numbers from the JSON array
            while (reader.Read() && reader.TokenType == JsonTokenType.Number)
            {
                angles.Add(new Angle(reader.GetDouble()));
            }

            // Ensure there are exactly 4 angles (top-right and bottom-left coordinates)
            if (angles.Count != _length)
            {
                throw new ArgumentException(string.Format(ExceptionsResources.ArgumentException_InvalidNumberOfElements, nameof(angles), angles.Count, _length));
            }

            // Map the angles to the BoundingBox properties
            return new BoundingBox
            {
                TopRightCoordinates = new Coordinates
                {
                    Latitude = angles[1],  // Second value represents latitude of top-right
                    Longitude = angles[0]  // First value represents longitude of top-right
                },
                BottomLeftCoordinates = new Coordinates
                {
                    Latitude = angles[3],  // Fourth value represents latitude of bottom-left
                    Longitude = angles[2]  // Third value represents longitude of bottom-left
                }
            };
        }

        /// <summary>
        /// Writes a <see cref="BoundingBox"/> object to JSON.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The <see cref="BoundingBox"/> object to serialize.</param>
        /// <param name="options">The serializer options to use for serialization.</param>
        public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var coordinates in value)
            {
                foreach (var angle in coordinates)
                {
                    writer.WriteNumberValue(angle.Degrees);
                }
            }

            writer.WriteEndArray();
        }
    }
}
