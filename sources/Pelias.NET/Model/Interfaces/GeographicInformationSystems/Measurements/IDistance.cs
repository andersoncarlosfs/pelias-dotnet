using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements
{
    /// <summary>
    /// Represents an interface for calculating geographical distance between two points,
    /// extending the general measurement interface.
    /// </summary>
    /// <typeparam name="TCoordinates">The type representing the coordinates of the points.</typeparam>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    /// <typeparam name="TLength">The type representing a length measurement.</typeparam>
    public interface IDistance<TCoordinates, TAngle, TLength> : IMeasurement
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
        where TLength : ILength
    {
        /// <summary>
        /// Radius at equator in meters (World Geodetic System 1984).
        /// </summary>
        public const double MEAN_EARTH_RADIUS = 6371009.0;

        /// <summary>
        /// Computes the geographical distance between two points.
        /// </summary>
        /// <param name="source">The coordinates of the source point.</param>
        /// <param name="target">The coordinates of the target point.</param>
        /// <returns>The distance between the two points in the specified length unit.</returns>
        public TLength Compute(TCoordinates source, TCoordinates target);
    }
}
