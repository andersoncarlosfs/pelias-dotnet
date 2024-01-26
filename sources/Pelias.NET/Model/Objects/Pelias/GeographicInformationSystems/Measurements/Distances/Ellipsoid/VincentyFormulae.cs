using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Distances.Ellipsoid
{
    public class VincentyFormulae : IDistance<Coordinates, Angle, Length>
    {
        /// <summary>
        /// Radius at equator in meters (World Geodetic System 1984)
        /// </summary>
        public const double EARTH_EQUATORIAL_RADIUS = 6378137.0;

        /// <summary>
        /// Flattening of the ellipsoid (World Geodetic System 1984)
        /// </summary>
        private const double FLATTENING_EARTH_ELLIPSOID = 1 / 298.257223563;
        private const double FLATTENING_EARTH_ELLIPSOID_COMPLEMENT = 1 - FLATTENING_EARTH_ELLIPSOID;
        /// <summary>
        /// Length of semi-minor axis of the ellipsoid (radius at the poles)
        /// </summary>
        private const double SEMI_MINOR_AXIS_EARTH_ELLIPSOID = FLATTENING_EARTH_ELLIPSOID_COMPLEMENT * EARTH_EQUATORIAL_RADIUS;
        public const int ITERATIONS = 200;
        public const double TOLERANCE = 1E-11;


        public Length Compute(Coordinates source, Coordinates target)
        {
            return new Length(Compute(source, target, ITERATIONS, TOLERANCE));
        }

        /// <summary>
        /// Returns the geographical distance and azimuth between two given points using the inverse method of the formulae published by Thaddeus Vincenty
        /// </summary>
        public double Compute(Coordinates source, Coordinates target, int iterations = ITERATIONS, double tolerance = TOLERANCE)
        {
            // https://nathanrooy.github.io/posts/2016-12-18/vincenty-formula-with-python/
            // https://www.johndcook.com/blog/2018/11/24/spheroid-distance/
            double longitude = target.Longitude.Radians - source.Longitude.Radians;

            double lambda = longitude;

            double source_phi = Math.Atan(FLATTENING_EARTH_ELLIPSOID_COMPLEMENT * Math.Tan(source.Latitude.Radians));
            double target_phi = Math.Atan(FLATTENING_EARTH_ELLIPSOID_COMPLEMENT * Math.Tan(target.Latitude.Radians));

            double source_phi_sine = Math.Sin(source_phi);
            double target_phi_sine = Math.Sin(target_phi);

            double source_phi_cosine = Math.Cos(source_phi);
            double target_phi_cosine = Math.Cos(target_phi);

            int? counter = iterations;

            while (true)
            {
                double lambda_sine = Math.Sin(lambda);

                double lambda_consine = Math.Cos(lambda);

                double sigma_sine = Math.Sqrt(Math.Pow(target_phi_cosine * lambda_sine, 2) + Math.Pow(source_phi_cosine * target_phi_sine - source_phi_sine * target_phi_cosine * lambda_consine, 2));

                double sigma_cosine = source_phi_sine * target_phi_sine + source_phi_cosine * target_phi_cosine * lambda_consine;

                double sigma = Math.Atan2(sigma_sine, sigma_cosine);

                double alpha_sine = source_phi_cosine * target_phi_cosine * sigma_sine / sigma_sine;

                double complent_squared_alpha_cosine = 1 - Math.Pow(alpha_sine, 2);

                double double_sigma_m_cosine = sigma_cosine - 2 * source_phi_sine * target_phi_sine / complent_squared_alpha_cosine;

                double c = FLATTENING_EARTH_ELLIPSOID / 16 * complent_squared_alpha_cosine * (4 + FLATTENING_EARTH_ELLIPSOID * (4 - 3 * complent_squared_alpha_cosine));

                double squared_double_sigma_m_cosine = Math.Pow(double_sigma_m_cosine, 2);

                double temporary = longitude + (1 - c) * FLATTENING_EARTH_ELLIPSOID * alpha_sine * (sigma + c * sigma_sine * (double_sigma_m_cosine + c * sigma_cosine * (-1 + 2 * squared_double_sigma_m_cosine)));

                if (Math.Abs(temporary - lambda) <= tolerance | (counter != null && --counter > 0))
                {
                    double squared_semi_minor_axis_ellipsoid = Math.Pow(SEMI_MINOR_AXIS_EARTH_ELLIPSOID, 2);

                    double reduced_latitude = complent_squared_alpha_cosine * (Math.Pow(EARTH_EQUATORIAL_RADIUS, 2) - squared_semi_minor_axis_ellipsoid) / squared_semi_minor_axis_ellipsoid;

                    double a = 1 + reduced_latitude / 16384 * (4096 + reduced_latitude * (-768 + reduced_latitude * (320 - 175 * reduced_latitude)));

                    double b = reduced_latitude / 1024 * (256 + reduced_latitude * (-128 + reduced_latitude * (74 - 47 * reduced_latitude)));

                    double sigma_delta = b * sigma_sine * (double_sigma_m_cosine + 0.25 * b * (sigma_cosine * (-1 + 2 * squared_double_sigma_m_cosine) - b / 6 * double_sigma_m_cosine * (-3 + 4 * Math.Pow(sigma_sine, 2)) * (-3 + 4 * squared_double_sigma_m_cosine)));

                    return SEMI_MINOR_AXIS_EARTH_ELLIPSOID * a * (sigma - sigma_delta);
                }

                lambda = temporary;
            }
        }
    }
}
