using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for converting BoundingBox to and from JSON.
    /// </summary>
    public class BoundingBoxConverter : JsonConverter<BoundingBox>
    {
        /// <summary>
        /// Reads JSON and converts it to a BoundingBox object.
        /// </summary>
        public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            // Start the parsing process and return the resulting BoundingBox
            => GetBoundingBox(ref reader, JsonTokenType.Number, new List<double>(), 4);

        /// <summary>
        /// Recursive method to read and build a BoundingBox object from JSON.
        /// </summary>
        private BoundingBox GetBoundingBox(ref Utf8JsonReader reader, JsonTokenType type, IList<double> angles = null, int? limit = null)
        {
            // Reading the next token
            reader.Read();

            // Getting the current token type
            var current = reader.TokenType;

            // Checking the current token type
            if (current.Equals(JsonTokenType.EndArray))
            {
                return new BoundingBox()
                {
                    TopRightCoordinates = new Coordinates()
                    {
                        Latitude = new Angle(angles[1]),
                        Longitude = new Angle(angles[0])
                    },
                    BottomLeftCoordinates = new Coordinates()
                    {
                        Latitude = new Angle(angles[3]),
                        Longitude = new Angle(angles[2])
                    }
                };
            }

            if (!current.Equals(type))
            {
                throw new TypeMismatchException(string.Format(ExceptionsResources.TypeMismatchException_NotEqual_JsonTokenType, nameof(reader), current, type, nameof(type)));
            }

            if (limit != null && angles.Count > limit)
            {
                throw new CollectionIterationException(string.Format(ExceptionsResources.CollectionIterationException, nameof(angles), angles.Count, limit));
            }

            // Getting the value of the token
            angles.Add(reader.GetDouble());

            // Continue the recursive process
            return GetBoundingBox(ref reader, type, angles, limit);
        }

        /// <summary>
        /// Writes a BoundingBox object to JSON.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            // Write each entry in the BoundingBox
            foreach (var entry in value.ToArray())
            {
                writer.WriteNumberValue(entry);
            }

            writer.WriteEndArray();
        }
    }
}
