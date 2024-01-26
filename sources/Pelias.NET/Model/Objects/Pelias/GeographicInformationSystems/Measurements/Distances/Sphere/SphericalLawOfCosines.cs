using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Distances.Sphere
{
    public class SphericalLawOfCosines : IDistance<Coordinates, Angle, Length>
    {
        /// <summary>
        /// Returns the geographical distance between two points using the great-circle formula   
        /// </summary>
        public Length Compute(Coordinates source, Coordinates target)
        {
            // https://medium.com/@petehouston/calculate-distance-of-two-locations-on-earth-using-python-1501b1944d97
            return new Length(IDistance<Coordinates, Angle, Length>.MEAN_EARTH_RADIUS * Math.Acos(Math.Sin(source.Latitude.Radians) * Math.Sin(target.Latitude.Radians) + Math.Cos(source.Latitude.Radians) * Math.Cos(target.Latitude.Radians) * Math.Cos(source.Longitude.Radians - target.Longitude.Radians)));
        }
    }
}
