using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for geographic coordinates defined by longitude and latitude,
    /// extending the general entity interface.
    /// </summary>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    public interface ICoordinates<TAngle> : IEntity
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the longitude of the coordinates.
        /// </summary>
        public TAngle Longitude { get; }

        /// <summary>
        /// Gets the latitude of the coordinates.
        /// </summary>
        public TAngle Latitude { get; }
    }
}
