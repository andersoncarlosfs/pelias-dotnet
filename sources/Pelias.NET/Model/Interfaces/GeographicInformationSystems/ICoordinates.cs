using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;
using System.Collections;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for geographic coordinates defined by longitude and latitude.
    /// This interface extends <see cref="IEntity"/> and implements <see cref="IEnumerable{TAngle}"/>.
    /// </summary>
    /// <typeparam name="TAngle">
    /// The type representing an angle measurement, which must implement <see cref="IAngle"/>.
    /// </typeparam>
    public interface ICoordinates<TAngle> : IEntity, IEnumerable<TAngle>
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the longitude of the geographic coordinates.
        /// </summary>
        /// <value>
        /// The longitude as a <typeparamref name="TAngle"/> representing the angle of the longitude.
        /// </value>
        TAngle Longitude { get; }

        /// <summary>
        /// Gets the latitude of the geographic coordinates.
        /// </summary>
        /// <value>
        /// The latitude as a <typeparamref name="TAngle"/> representing the angle of the latitude.
        /// </value>
        TAngle Latitude { get; }

        /// <summary>
        /// Returns an enumerator that iterates through the geographic coordinates (longitude and latitude).
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate over the <see cref="Longitude"/> and <see cref="Latitude"/> values.
        /// </returns>
        /// <remarks>
        /// This is an explicit implementation of <see cref="IEnumerable{TAngle}"/>. It enables the use of a 
        /// <c>foreach</c> loop to iterate over the geographic coordinates, yielding both <see cref="Longitude"/>
        /// and <see cref="Latitude"/> values.
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
