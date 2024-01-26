using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;

namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for a geographic feature, extending the general entity interface,
    /// with properties, geometry, and bounding box information.
    /// </summary>
    /// <typeparam name="TProperties">The type representing properties of the feature.</typeparam>
    /// <typeparam name="TGeometry">The type representing the geometry of the feature.</typeparam>
    /// <typeparam name="TBoundingBox">The type representing the bounding box of the feature.</typeparam>
    /// <typeparam name="TCoordinates">The type representing the coordinates of the feature.</typeparam>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    public interface IFeature<TProperties, TGeometry, TBoundingBox, TCoordinates, TAngle> : IEntity
        where TProperties : IProperties
        where TGeometry : IGeometry<TBoundingBox, TCoordinates, TAngle>
        where TBoundingBox : IBoundingBox<TCoordinates, TAngle>
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
    {
        /// <summary>
        /// Gets the type of the feature.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the properties of the feature.
        /// </summary>
        public TProperties Properties { get; }

        /// <summary>
        /// Gets the geometry of the feature.
        /// </summary>
        public TGeometry Geometry { get; }

        /// <summary>
        /// Gets the bounding box of the feature.
        /// </summary>
        public TBoundingBox BoundingBox { get; }
    }
}
