using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using System.Text.Json;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for <see cref="BoundingBox"/>, handling the serialization and deserialization of a bounding box.
    /// This converter inherits from <see cref="AnglesConverter"/> and is responsible for converting a list of angles to a <see cref="BoundingBox"/> object.
    /// </summary>
    public class BoundingBoxConverter : AnglesConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBoxConverter"/> class.
        /// </summary>
        public BoundingBoxConverter() : base(4)
        {
        }

        /// <summary>
        /// Reads a JSON value and converts it into a <see cref="BoundingBox"/> object.
        /// This method overrides the base <see cref="AnglesConverter.Read"/> method to map the first two angles to the top-right coordinates
        /// and the last two angles to the bottom-left coordinates.
        /// </summary>
        /// <param name="reader">The JSON reader to read the value from.</param>
        /// <param name="typeToConvert">The target type to convert to (<see cref="BoundingBox"/>).</param>
        /// <param name="options">The serializer options to use for deserialization.</param>
        /// <returns>A <see cref="BoundingBox"/> object representing the deserialized bounding box.</returns>
        /// <remarks>
        /// The input JSON is expected to be an array with four values: the first and second for the top-right coordinates (longitude, latitude),
        /// and the third and fourth for the bottom-left coordinates (longitude, latitude).
        /// </remarks>
        public new BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize the angles array from the JSON reader
            var angles = base.Read(ref reader, typeToConvert, options);

            // Create and return a BoundingBox object with the corresponding coordinates
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
        /// This method overrides the base <see cref="AnglesConverter.Write"/> method to serialize the bounding box as an array of four angles.
        /// </summary>
        /// <param name="writer">The JSON writer to write the value to.</param>
        /// <param name="value">The <see cref="BoundingBox"/> object to serialize.</param>
        /// <param name="options">The serializer options to use for serialization.</param>
        /// <remarks>
        /// The <see cref="BoundingBox"/> object will be serialized as an array of four values: 
        /// the first two represent the top-right coordinates (longitude, latitude), and the last two represent the bottom-left coordinates (longitude, latitude).
        /// </remarks>
        public new void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            // Serialize the BoundingBox object by flattening the coordinates into a list of angles and writing them as JSON
            base.Write(writer, value.SelectMany(e => e.ToList()).ToList(), options);
        }
    }
}
