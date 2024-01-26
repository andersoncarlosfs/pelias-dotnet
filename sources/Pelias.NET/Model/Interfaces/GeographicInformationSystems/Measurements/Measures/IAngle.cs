namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures
{
    /// <summary>
    /// Represents an interface for an angle measurement, extending the general measurement interface.
    /// </summary>
    public interface IAngle : IMeasure
    {
        /// <summary>
        /// Gets the angle value in degrees.
        /// </summary>
        public double Degrees { get; }
    }
}
