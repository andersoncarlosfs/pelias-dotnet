using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    public abstract class AnglesConverter : JsonConverter<List<Angle>>
    {
        private readonly int? _limit;

        public AnglesConverter(int limit)
        {
            _limit = limit;
        }

        public override List<Angle> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var angles = new List<Angle>();

            while (reader.Read() && reader.TokenType == JsonTokenType.Number)
            {
                // Add the current token value to the angles list
                angles.Add(new Angle(reader.GetDouble()));
            };

            // Check if the number of parsed angles exceeds the limit
            if (_limit.HasValue && (angles.Count != _limit.Value))
            {
                throw new CollectionIterationException(
                    string.Format(
                        ExceptionsResources.CollectionIterationException,
                        nameof(angles),
                        angles.Count,
                        _limit.Value
                    )
                );
            }

            return angles;
        }

        public override void Write(Utf8JsonWriter writer, List<Angle> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var angle in value)
            {
                writer.WriteNumberValue(angle.Degrees);
            }

            // End the JSON array
            writer.WriteEndArray();
        }
    }
}
