using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Collections;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    /// <summary>
    /// Represents a geographical coordinate consisting of longitude and latitude, both expressed as <see cref="Angle"/> values.
    /// Implements <see cref="IEnumerable{Angle}"/> to enable iteration over the coordinate values.
    /// </summary>
    public class Coordinates : ICoordinates<Angle>
    {
        /// <summary>
        /// Gets or sets the longitude of the geographic location.
        /// Longitude is represented as an <see cref="Angle"/> value, expressed in degrees.
        /// </summary>
        public Angle Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the geographic location.
        /// Latitude is represented as an <see cref="Angle"/> value, expressed in degrees.
        /// </summary>
        public Angle Latitude { get; set; }

        /// <summary>
        /// Converts the geographic coordinates to an array of doubles, where the first element is the longitude
        /// and the second element is the latitude, both in degrees.
        /// </summary>
        /// <returns>A double array containing the longitude and latitude values in degrees.</returns>
        /// <remarks>
        /// This method provides a simple conversion of the <see cref="Longitude"/> and <see cref="Latitude"/>
        /// properties to an array of double values. The longitude is the first element, followed by latitude.
        /// </remarks>
        public double[] ToArray()
        {
            // Return the longitude and latitude as an array of doubles.
            return new[] { Longitude.Degrees, Latitude.Degrees };
        }

        /// <summary>
        /// Returns an enumerator that iterates through the geographic coordinates.
        /// </summary>
        /// <returns>An enumerator for the <see cref="Longitude"/> and <see cref="Latitude"/> values.</returns>
        public IEnumerator<Angle> GetEnumerator()
        {
            // Yield the longitude and latitude as part of the iteration process.
            yield return Longitude;
            yield return Latitude;
        }
    }
}
