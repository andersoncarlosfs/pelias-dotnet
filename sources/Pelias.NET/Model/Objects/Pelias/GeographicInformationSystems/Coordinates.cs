using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    public class Coordinates : ICoordinates<Angle>
    {
        public Angle Longitude { get; set; }
        public Angle Latitude { get; set; }

        public double[] ToArray()
            => new double[] {
                Longitude.Degrees,
                Latitude.Degrees
            };
    }
}