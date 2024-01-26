using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for geographic geometry, extending the general entity interface,
    /// with coordinates information.
    /// </summary>
    /// <typeparam name="TBoundingBox">The type representing the bounding box of the geometry.</typeparam>
    /// <typeparam name="TCoordinates">The type representing the coordinates of the geometry.</typeparam>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    public interface IGeometry<TBoundingBox, TCoordinates, TAngle> : IEntity
        where TBoundingBox : IBoundingBox<TCoordinates, TAngle>
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the coordinates of the geometry.
        /// </summary>
        public TCoordinates Coordinates { get; }
    }
}
