namespace Pelias.NET.Model.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when there is a type mismatch during an operation.
    /// </summary>
    [Serializable]
    public class TypeMismatchException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMismatchException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public TypeMismatchException(string message) : base(message)
        {
        }
    }
}
