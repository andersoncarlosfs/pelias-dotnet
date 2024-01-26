namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures
{
    /// <summary>
    /// Represents an interface for a length measurement, extending the general measurement interface.
    /// </summary>
    public interface ILength : IMeasure
    {
        /// <summary>
        /// Conversion factor: 1 mile in meters.
        /// </summary>
        public const double MILE = 1609.344;

        /// <summary>
        /// Gets the length value in meters.
        /// </summary>
        public double Meters { get; }

        /// <summary>
        /// Gets the length value in miles, calculated based on the conversion factor.
        /// </summary>
        public double Miles { get => Meters / MILE; }
    }
}
