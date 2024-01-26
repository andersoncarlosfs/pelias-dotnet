namespace Pelias.NET.Model.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a required entry is missing.
    /// </summary>
    [Serializable]
    public class MissingEntryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingEntryException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public MissingEntryException(string message) : base(message)
        {
        }
    }
}
