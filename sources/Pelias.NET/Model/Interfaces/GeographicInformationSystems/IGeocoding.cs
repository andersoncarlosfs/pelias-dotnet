namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for geocoding information, extending the general entity interface.
    /// </summary>
    public interface IGeocoding : IEntity
    {
        /// <summary>
        /// Gets the version of the geocoding information.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets the attribution information for the geocoding data.
        /// </summary>
        public string Attribution { get; }

        /// <summary>
        /// Gets the timestamp of the geocoding information.
        /// </summary>
        public long Timestamp { get; }
    }
}
