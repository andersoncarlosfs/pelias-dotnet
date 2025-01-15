using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for <see cref="Coordinates"/>, handling the serialization and deserialization of geographic coordinates.
    /// This converter is responsible for converting a list of angles to a <see cref="Coordinates"/> object.
    /// </summary>
    public class CoordinatesConverter : JsonConverter<Coordinates>
    {
        private static readonly int _length = new Coordinates().Count();

        /// <summary>
        /// Reads a JSON value and converts it into a <see cref="Coordinates"/> object.
        /// This method maps the first angle to longitude and the second angle to latitude.
        /// </summary>
        /// <param name="reader">The JSON reader to read the value from.</param>
        /// <param name="typeToConvert">The target type to convert to (<see cref="Coordinates"/>).</param>
        /// <param name="options">The serializer options to use for deserialization.</param>
        /// <returns>A <see cref="Coordinates"/> object representing the deserialized geographic coordinates.</returns>
        /// <remarks>
        /// The input JSON is expected to be an array with two values: the first being the longitude and the second being the latitude.
        /// </remarks>
        public override Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var angles = new List<Angle>();

            // Read the JSON array and add the angle values to the list
            while (reader.Read() && reader.TokenType == JsonTokenType.Number)
            {
                angles.Add(new Angle(reader.GetDouble()));
            }

            // Check if we got exactly two values (longitude and latitude)
            if (angles.Count != _length)
            {
                throw new ArgumentException(string.Format(ExceptionsResources.ArgumentException_InvalidNumberOfElements, nameof(angles), angles.Count, _length));
            }

            // Return a new Coordinates object with the correct latitude and longitude values
            return new Coordinates()
            {
                Latitude = angles[1],    // Second value is latitude
                Longitude = angles[0]  // First value is longitude
            };
        }

        /// <summary>
        /// Writes a <see cref="Coordinates"/> object to JSON.
        /// This method serializes the longitude and latitude as a list of angles.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The <see cref="Coordinates"/> object to serialize.</param>
        /// <param name="options">The serializer options to use for serialization.</param>
        public override void Write(Utf8JsonWriter writer, Coordinates value, JsonSerializerOptions options)
        {
            // Start the JSON array
            writer.WriteStartArray();

            // Write the longitude and latitude values as angle degrees
            foreach (var angle in value)
            {
                writer.WriteNumberValue(angle.Degrees);
            }

            // End the JSON array
            writer.WriteEndArray();
        }
    }
}
