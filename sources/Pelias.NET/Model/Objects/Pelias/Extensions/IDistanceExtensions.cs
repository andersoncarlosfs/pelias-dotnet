using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Resources;

namespace Pelias.NET.Model.Objects.Pelias.Extensions
{
    /// <summary>
    /// Provides extension methods for computing distances using the <see cref="IDistance{Coordinates, Angle, Length}"/> interface.
    /// </summary>
    public static class IDistanceExtensions
    {
        /// <summary>
        /// Computes the total distance between a series of coordinates.
        /// This method calculates the distance between each consecutive pair of coordinates and sums the results.
        /// </summary>
        /// <typeparam name="T">The type that implements the <see cref="IDistance{Coordinates, Angle, Length}"/> interface.</typeparam>
        /// <param name="value">The instance that provides the distance computation logic.</param>
        /// <param name="coordinates">An array of coordinates between which the distance is to be computed.</param>
        /// <returns>A <see cref="Length"/> representing the total computed distance in meters.</returns>
        /// <exception cref="ArgumentException">Thrown when fewer than two coordinates are provided.</exception>
        public static Length Compute<T>(this T value, params Coordinates[] coordinates)
            where T : IDistance<Coordinates, Angle, Length>
        {
            // Validate that at least two coordinates are provided to calculate the distance.
            if (coordinates.Length < 2)
            {
                throw new ArgumentException(
                    string.Format(
                        ExceptionsResources.ArgumentException_InsufficientNumberOfElements,
                        nameof(coordinates),
                        coordinates.Length,
                        2));
            }

            // Calculate the total distance by summing up the distance between each consecutive pair of coordinates.
            return new Length(
                coordinates
                    .Zip(coordinates.Skip(1), Tuple.Create) // Pair up consecutive coordinates.
                    .Sum(tuple => value.Compute(tuple.Item1, tuple.Item2).Meters) // Compute the distance and sum.
            );
        }
    }
}
