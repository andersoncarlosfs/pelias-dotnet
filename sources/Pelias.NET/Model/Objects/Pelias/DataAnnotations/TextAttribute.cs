using Pelias.NET.Model.Objects.Pelias.Resources;
using System.ComponentModel.DataAnnotations;

namespace Pelias.NET.Model.Objects.Pelias.DataAnnotations
{
    /// <summary>
    /// Represents a custom data annotation attribute for validating string values with optional whitespace handling.
    /// This attribute extends <see cref="StringLengthAttribute"/> and adds the ability to control whether whitespace is allowed in the string.
    /// </summary>
    public class TextAttribute : StringLengthAttribute
    {
        /// <summary>
        /// Gets a value that indicates whether whitespace is allowed in the string.
        /// The default value is <c>false</c>, meaning whitespace is not allowed unless explicitly enabled.
        /// </summary>
        public bool AllowWhitespace { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAttribute"/> class.
        /// The default value for <see cref="AllowWhitespace"/> is <c>false</c>, meaning whitespace is not allowed by default.
        /// The <paramref name="maximumLength"/> parameter sets the maximum allowed string length.
        /// </summary>
        /// <param name="allowWhitespace">Indicates whether whitespace should be allowed in the string (default is <c>false</c>).</param>
        /// <param name="maximumLength">The maximum allowable length of the string. Defaults to <see cref="int.MaxValue"/> if not specified.</param>
        public TextAttribute(bool allowWhitespace = false, int maximumLength = int.MaxValue)
            : base(maximumLength) // Sets the maximum length to the largest possible integer value
        {
            this.AllowWhitespace = allowWhitespace; // Default behavior is to disallow whitespace
        }

        /// <summary>
        /// Validates whether the specified value is valid based on the string length and optional whitespace rules.
        /// </summary>
        /// <param name="value">The value to validate. If <c>null</c>, validation passes.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// If <see cref="AllowWhitespace"/> is <c>false</c>, the method checks that the value does not consist solely of whitespace.
        /// It also ensures that the string length does not exceed the maximum length defined by the base class.
        /// </remarks>
        public override bool IsValid(object value)
        {
            // Check if the value is not null and whitespace is not allowed
            if (value != null && !AllowWhitespace && string.IsNullOrWhiteSpace(value.ToString()))
            {
                // Set a custom error message if the value consists only of whitespace
                ErrorMessage = DataAnnotationsResources.TextAttribute_ValidationError;

                return false; // Return false to indicate validation failure
            }

            // Perform base validation (checks string length)
            return base.IsValid(value);
        }
    }
}
