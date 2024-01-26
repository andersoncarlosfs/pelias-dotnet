namespace Pelias.NET.Model.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when there is an issue during the iteration of a collection.
    /// </summary>
    [Serializable]
    public class CollectionIterationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionIterationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CollectionIterationException(string message) : base(message)
        {
        }
    }
}
