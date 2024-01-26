namespace Pelias.NET.Model.Interfaces.GeographicInformationSystems
{
    /// <summary>
    /// Represents an interface for properties of a geographic entity, extending the general entity interface.
    /// </summary>
    public interface IProperties : IEntity
    {
        /// <summary>
        /// Gets the identifier of the geographic entity.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Gets the group identifier of the geographic entity.
        /// </summary>
        public string GroupIdentifier { get; }

        /// <summary>
        /// Gets the layer of the geographic entity.
        /// </summary>
        public string Layer { get; }

        /// <summary>
        /// Gets the source of the geographic entity.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Gets the source identifier of the geographic entity.
        /// </summary>
        public string SourceIdentifier { get; }

        /// <summary>
        /// Gets the name of the geographic entity.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the confidence level of the geographic entity.
        /// </summary>
        public double Confidence { get; }

        /// <summary>
        /// Gets the accuracy information of the geographic entity.
        /// </summary>
        public string Accuracy { get; }

        /// <summary>
        /// Gets the label of the geographic entity.
        /// </summary>
        public string Label { get; }
    }
}
