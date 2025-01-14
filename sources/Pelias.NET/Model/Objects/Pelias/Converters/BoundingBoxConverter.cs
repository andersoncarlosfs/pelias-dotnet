using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    public class BoundingBoxConverter : AnglesConverter
    {
        public BoundingBoxConverter() : base(4)
        {
        }

        public new BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var angles = base.Read(ref reader, typeToConvert, options);

            return new BoundingBox
            {
                TopRightCoordinates = new Coordinates
                {
                    Latitude = angles[1],
                    Longitude = angles[0]
                },
                BottomLeftCoordinates = new Coordinates
                {
                    Latitude = angles[3],
                    Longitude = angles[2]
                }
            };
        }

        public new void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
        {
            base.Write(writer, value.SelectMany(e => e.ToList()).ToList(), options);
        }
    }
}
