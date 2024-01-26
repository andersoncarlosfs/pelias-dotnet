using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.Extensions
{
    public static class IDistanceExtensions
    {
        public static Length Compute<T>(this T value, params Coordinates[] coordinates)
            where T : IDistance<Coordinates, Angle, Length>
        {
            if (coordinates.Length < 2)
            {
                throw new ArgumentException($"The collection '{nameof(coordinates)}' has '{coordinates.Length}' elements instead of at least '2'.");
            }

            return new Length(coordinates.Zip(coordinates.Skip(1), Tuple.Create).Sum(tuple => value.Compute(tuple.Item1, tuple.Item2).Meters));
        }
    }
}
