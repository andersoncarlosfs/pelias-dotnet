using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Interfaces.GeographicInformationSystems.Measurements.Measures;
using Pelias.NET.Model.Interfaces.Protocols.Http;

namespace Pelias.NET.Model.Interfaces
{
    /// <summary>
    /// Represents a generic interface for Pelias API clients with specified generic types.
    /// </summary>
    /// <typeparam name="TResponse">The type representing the response from Pelias API.</typeparam>
    /// <typeparam name="TGeocoding">The type representing geocoding information in the response.</typeparam>
    /// <typeparam name="TFeature">The type representing a feature in the response.</typeparam>
    /// <typeparam name="TProperties">The type representing properties of a feature.</typeparam>
    /// <typeparam name="TGeometry">The type representing the geometry of a feature.</typeparam>
    /// <typeparam name="TBoundingBox">The type representing the bounding box of a feature.</typeparam>
    /// <typeparam name="TCoordinates">The type representing the coordinates of a feature.</typeparam>
    /// <typeparam name="TAngle">The type representing an angle measurement.</typeparam>
    public interface IClient<TResponse, TGeocoding, TFeature, TProperties, TGeometry, TBoundingBox, TCoordinates, TAngle>
        where TResponse : IResponse<TGeocoding, TFeature, TProperties, TGeometry, TBoundingBox, TCoordinates, TAngle>
        where TGeocoding : IGeocoding
        where TFeature : IFeature<TProperties, TGeometry, TBoundingBox, TCoordinates, TAngle>
        where TProperties : IProperties
        where TGeometry : IGeometry<TBoundingBox, TCoordinates, TAngle>
        where TBoundingBox : IBoundingBox<TCoordinates, TAngle>
        where TCoordinates : ICoordinates<TAngle>
        where TAngle : IAngle
    {
    }
}
