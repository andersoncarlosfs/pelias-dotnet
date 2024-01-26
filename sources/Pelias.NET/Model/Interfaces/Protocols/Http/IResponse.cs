using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Interfaces.Protocols.Http
{
    /// <summary>
    /// Represents a generic interface for a Pelias API response with specified generic types,
    /// extending the general entity interface.
    /// </summary>
    /// <typeparam name="TGeocoding">The type representing geocoding information in the response.</typeparam>
    /// <typeparam name="TFeature">The type representing a feature in the response.</typeparam>
    /// <typeparam name="TProperties">The type representing properties of a feature.</typeparam>
    /// <typeparam name="TGeometry">The type representing the geometry of a feature.</typeparam>
    /// <typeparam name="TBoundingBox">The type representing the bounding box of a feature.</typeparam>
    /// <typeparam name="TCoordinates">The type representing the coordinates of a feature.</typeparam>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    public interface IResponse<TGeocoding, TFeature, TProperties, TGeometry, TBoundingBox, TCoordinates, TAngle> : IEntity
        where TGeocoding : IGeocoding
        where TFeature : IFeature<TProperties, TGeometry, TBoundingBox, TCoordinates, TAngle>
        where TProperties : IProperties
        where TGeometry : IGeometry<TBoundingBox, TCoordinates, TAngle>
        where TBoundingBox : IBoundingBox<TCoordinates, TAngle>
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the geocoding information of the response.
        /// </summary>
        public TGeocoding Geocoding { get; }

        /// <summary>
        /// Gets the type of the response.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets a list of features in the response.
        /// </summary>
        public IList<TFeature> Features { get; }

        /// <summary>
        /// Gets or sets the bounding box of the response.
        /// </summary>
        public TBoundingBox BoundingBox { get; }
    }
}
