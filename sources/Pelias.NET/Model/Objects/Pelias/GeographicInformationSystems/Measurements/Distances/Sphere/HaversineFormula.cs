using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Distances.Sphere
{
    public class HaversineFormula : IDistance<Coordinates, Angle, Length>
    {
        /// <summary>
        /// Returns the geographical distance as the great-circle distance between two points using the haversine formula   
        /// </summary>
        public Length Compute(Coordinates source, Coordinates target)
        {
            // https://medium.com/@petehouston/calculate-distance-of-two-locations-on-earth-using-python-1501b1944d97
            double squared_longitude_delta_sine = Math.Pow(Math.Sin((target.Longitude.Radians - source.Longitude.Radians) / 2), 2);
            double squared_latitude_delta_sine = Math.Pow(Math.Sin((target.Latitude.Radians - source.Latitude.Radians) / 2), 2);

            return new Length(2 * IDistance<Coordinates, Angle, Length>.MEAN_EARTH_RADIUS * Math.Asin(Math.Sqrt(squared_latitude_delta_sine + Math.Cos(source.Latitude.Radians) * Math.Cos(target.Latitude.Radians) * squared_longitude_delta_sine)));
        }
    }
}
