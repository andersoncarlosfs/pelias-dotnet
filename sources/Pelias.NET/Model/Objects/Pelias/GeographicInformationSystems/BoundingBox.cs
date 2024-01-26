using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class BoundingBox : IBoundingBox<Coordinates, Angle>
    {
        [JsonPropertyName("northeast")]
        public Coordinates TopRightCoordinates { get; set; }
        [JsonPropertyName("southwest")]
        public Coordinates BottomLeftCoordinates { get; set; }

        public double[] ToArray()
            => (new double[][] {
                TopRightCoordinates.ToArray(),
                BottomLeftCoordinates.ToArray()
            }).SelectMany(entry => entry).ToArray();
    }
}
