using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using System.Text.Json;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    public class CoordinatesConverter : AnglesConverter
    {
        public CoordinatesConverter() : base(2)
        {
        }

        public new Coordinates Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var angles = base.Read(ref reader, typeToConvert, options);

            return new Coordinates()
            {
                Latitude = angles[1],
                Longitude = angles[0]
            };
        }

        public new void Write(Utf8JsonWriter writer, Coordinates value, JsonSerializerOptions options)
        {
            base.Write(writer, value.ToList(), options);
        }
    }
}
