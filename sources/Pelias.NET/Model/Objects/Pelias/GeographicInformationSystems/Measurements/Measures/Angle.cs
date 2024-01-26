using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures
{
    public class Angle : IAngle
    {
        /// <summary>
        /// Radian in degree
        /// </summary>
        public const double RADIAN = 180 / Math.PI;

        public double Degrees { get; set; }
        public double Radians
        {
            get => Degrees / RADIAN;
            set => Degrees = value * RADIAN;
        }

        public Angle(double degrees)
        {
            Degrees = degrees;
        }
    }
}
