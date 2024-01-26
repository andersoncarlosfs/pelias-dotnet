using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures
{
    public class Length : ILength
    {
        public double Meters { get; set; }
        public double Miles
        {
            get => Meters / 1609.344;
            set => Meters = value * ILength.MILE;
        }

        public Length(double meters)
        {
            Meters = meters;
        }
    }
}
