using Pelias.NET.Model.Interfaces.GeographicInformationSystems;
using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems
{
    /// <summary>
    /// Represents a bounding box with top-right and bottom-left coordinates.
    /// Implements the <see cref="IBoundingBox{Coordinates, Angle}"/> interface.
    /// </summary>
    public class BoundingBox : IBoundingBox<Coordinates, Angle>
    {
        /// <summary>
        /// Gets or sets the top-right coordinates (northeast corner) of the bounding box.
        /// </summary>
        /// <value>
        /// A <see cref="Coordinates"/> instance representing the top-right coordinates of the bounding box.
        /// </value>
        [JsonPropertyName("northeast")]
        public Coordinates TopRightCoordinates { get; set; }

        /// <summary>
        /// Gets or sets the bottom-left coordinates (southwest corner) of the bounding box.
        /// </summary>
        /// <value>
        /// A <see cref="Coordinates"/> instance representing the bottom-left coordinates of the bounding box.
        /// </value>
        [JsonPropertyName("southwest")]
        public Coordinates BottomLeftCoordinates { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the coordinates of the bounding box.
        /// </summary>
        /// <returns>
        /// An enumerator that yields <see cref="TopRightCoordinates"/> and <see cref="BottomLeftCoordinates"/>.
        /// </returns>
        /// <remarks>
        /// This method is an explicit implementation of the <see cref="IEnumerable{Coordinates}"/> interface.
        /// It allows iteration over the bounding box's coordinates using a <c>foreach</c> loop.
        /// </remarks>
        public IEnumerator<Coordinates> GetEnumerator()
        {
            yield return TopRightCoordinates;
            yield return BottomLeftCoordinates;
        }
    }
}
