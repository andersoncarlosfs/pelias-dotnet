using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for converting Coordinates to and from JSON.
    /// </summary>
    public class CoordinatesConverter : JsonConverter<Coordinates>
    {
        /// <summary>
        /// Recursive method to read and build a Coordinates object from JSON.
        /// </summary>
        private Coordinates GetCoordinates(ref Utf8JsonReader reader, JsonTokenType type, IList<double> angles = null, int? limit = null)
        {
            // Reading the next token
            reader.Read();

            // Getting the current token type
            var current = reader.TokenType;

            // Checking the current token type
            if (current.Equals(JsonTokenType.EndArray))
            {
                return new Coordinates()
                {
                    Latitude = new Angle(angles[1]),
                    Longitude = new Angle(angles[0])
                };
            }

            if (!current.Equals(type))
            {
                throw new TypeMismatchException(string.Format(ExceptionsResources.TypeMismatchException_NotEqual_JsonTokenType, nameof(reader), current, type, nameof(type)));
            }

            if (limit != null && angles.Count > limit)
            {
                throw new CollectionIterationException($"The collection '{nameof(angles)}' has '{angles.Count}' elements instead of '{limit}'.");
            }

            // Getting the value of the token
            angles.Add(reader.GetDouble());

            // Continue the recursive process
            return GetCoordinates(ref reader, type, angles, limit);
        }

        /// <summary>
        /// Reads JSON and converts it to a Coordinates object.
        /// </summary>
        public override Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Start the parsing process and return the resulting Coordinates
            return GetCoordinates(ref reader, JsonTokenType.Number, new List<double>(), 2);
        }

        /// <summary>
        /// Writes a Coordinates object to JSON.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Coordinates value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            // Write each entry in the Coordinates
            foreach (var entry in value.ToArray())
            {
                writer.WriteNumberValue(entry);
            }

            writer.WriteEndArray();
        }
    }
}
