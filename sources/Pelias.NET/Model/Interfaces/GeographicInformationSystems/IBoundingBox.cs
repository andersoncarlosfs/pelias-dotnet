using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for a bounding box defined by top-right and bottom-left coordinates,
    /// extending the general entity interface.
    /// </summary>
    /// <typeparam name="TCoordinates">The type representing the coordinates of the bounding box.</typeparam>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    public interface IBoundingBox<TCoordinates, TAngle> : IEntity
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the top-right coordinates of the bounding box.
        /// </summary>
        public TCoordinates TopRightCoordinates { get; }

        /// <summary>
        /// Gets the bottom-left coordinates of the bounding box.
        /// </summary>
        public TCoordinates BottomLeftCoordinates { get; }

        /// <summary>
        /// Checks whether the specified coordinates are within the bounding box.
        /// </summary>
        /// <param name="coordinates">The coordinates to check.</param>
        /// <returns>True if the coordinates are within the bounding box, otherwise false.</returns>
        public bool Contains(TCoordinates coordinates)
          => coordinates.Latitude.Degrees <= TopRightCoordinates.Latitude.Degrees && coordinates.Latitude.Degrees >= BottomLeftCoordinates.Latitude.Degrees && coordinates.Longitude.Degrees <= TopRightCoordinates.Longitude.Degrees && coordinates.Longitude.Degrees >= BottomLeftCoordinates.Longitude.Degrees;

    }
}
