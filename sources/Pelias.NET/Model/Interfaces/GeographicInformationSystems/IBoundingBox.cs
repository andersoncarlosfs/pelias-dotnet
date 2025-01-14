using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;
using System.Collections;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for a bounding box defined by top-right and bottom-left coordinates.
    /// This interface extends the <see cref="IEntity"/> interface and implements <see cref="IEnumerable{TCoordinates}"/>.
    /// </summary>
    /// <typeparam name="TCoordinates">
    /// The type representing the coordinates of the bounding box, which must implement <see cref="ICoordinates{TAngle}"/>.
    /// </typeparam>
    /// <typeparam name="TAngle">
    /// The type representing an angle measurement, which must implement <see cref="IAngle"/>.
    /// </typeparam>
    public interface IBoundingBox<TCoordinates, TAngle> : IEntity, IEnumerable<TCoordinates>
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the top-right coordinates of the bounding box.
        /// </summary>
        /// <value>
        /// The top-right coordinates as a <typeparamref name="TCoordinates"/> instance.
        /// </value>
        TCoordinates TopRightCoordinates { get; }

        /// <summary>
        /// Gets the bottom-left coordinates of the bounding box.
        /// </summary>
        /// <value>
        /// The bottom-left coordinates as a <typeparamref name="TCoordinates"/> instance.
        /// </value>
        TCoordinates BottomLeftCoordinates { get; }

        /// <summary>
        /// Returns an enumerator that iterates through the coordinates of the bounding box.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate over the top-right and bottom-left coordinates.
        /// </returns>
        /// <remarks>
        /// This is an explicit implementation of <see cref="IEnumerable{TCoordinates}"/>.
        /// It enables the use of a <c>foreach</c> loop to iterate over the bounding box's coordinates.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Checks whether the specified coordinates are within the bounding box.
        /// </summary>
        /// <param name="coordinates">
        /// The coordinates to check, represented as a <typeparamref name="TCoordinates"/> instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the coordinates are within the bounds of the bounding box, otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method checks if both the latitude and longitude of the specified coordinates 
        /// are within the ranges defined by the top-right and bottom-left coordinates of the bounding box.
        /// </remarks>
        bool Contains(TCoordinates coordinates)
          => coordinates.Latitude.Degrees <= TopRightCoordinates.Latitude.Degrees
             && coordinates.Latitude.Degrees >= BottomLeftCoordinates.Latitude.Degrees
             && coordinates.Longitude.Degrees <= TopRightCoordinates.Longitude.Degrees
             && coordinates.Longitude.Degrees >= BottomLeftCoordinates.Longitude.Degrees;
    }
}
