using System.Collections.Specialized;

namespace Pelias.NET.Model.Interfaces.Protocols.Http
{
    /// <summary>
    /// Represents an interface for an HTTP query with methods to create parameters for URL encoding.
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Creates a collection of parameters with their respective values for URL encoding.
        /// </summary>
        /// <returns>A collection of parameters for URL encoding.</returns>
        public NameValueCollection ToNameValueCollection();
    }
}
