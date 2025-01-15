using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using System.Text.Json;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for <see cref="Coordinates"/>, handling the serialization and deserialization of geographic coordinates.
    /// This converter inherits from <see cref="AnglesConverter"/> and is responsible for converting a list of angles to a <see cref="Coordinates"/> object.
    /// </summary>
    public class CoordinatesConverter : AnglesConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesConverter"/> class.
        /// </summary>
        public CoordinatesConverter() : base(2)
        {
        }

        /// <summary>
        /// Reads a JSON value and converts it into a <see cref="Coordinates"/> object.
        /// This method overrides the base <see cref="AnglesConverter.Read"/> method to map the first angle to longitude
        /// and the second angle to latitude.
        /// </summary>
        /// <param name="reader">The JSON reader to read the value from.</param>
        /// <param name="typeToConvert">The target type to convert to (<see cref="Coordinates"/>).</param>
        /// <param name="options">The serializer options to use for deserialization.</param>
        /// <returns>A <see cref="Coordinates"/> object representing the deserialized geographic coordinates.</returns>
        /// <remarks>
        /// The input JSON is expected to be an array with two values: the first being the longitude and the second being the latitude.
        /// </remarks>
        public new Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the angles array from the JSON reader
            var angles = base.Read(ref reader, typeToConvert, options);

            // Create a new Coordinates object and assign longitude and latitude
            return new Coordinates()
            {
                Latitude = angles[1],
                Longitude = angles[0]
            };
        }

        /// <summary>
        /// Writes a <see cref="Coordinates"/> object to JSON.
        /// This method overrides the base <see cref="AnglesConverter.Write"/> method to serialize the longitude and latitude
        /// as a list of angles.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The <see cref="Coordinates"/> object to serialize.</param>
        /// <param name="options">The serializer options to use for serialization.</param>
        /// <remarks>
        /// The <see cref="Coordinates"/> object will be serialized as an array of two values: longitude and latitude.
        /// </remarks>
        public new void Write(Utf8JsonWriter writer, Coordinates value, JsonSerializerOptions options)
        {
            // Serialize the Coordinates object by writing the values as a list of angles
            base.Write(writer, value.ToList(), options);
        }
    }
}
