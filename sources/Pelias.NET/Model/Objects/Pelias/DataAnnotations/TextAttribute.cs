using Pelias.NET.Model.Objects.Pelias.Resources;
using System.ComponentModel.DataAnnotations;

namespace Pelias.NET.Model.Objects.Pelias.DataAnnotations
{
    /// <summary>
    /// Represents a custom data annotation attribute for validating strings with optional whitespace handling.
    /// This attribute extends <see cref="StringLengthAttribute"/> and adds the ability to control whether whitespace is allowed.
    /// </summary>
    public class TextAttribute : StringLengthAttribute
    {
        /// <summary>
        /// Gets or sets a value that indicates whether whitespace is allowed in the string.
        /// The default is <c>false</c>, meaning whitespace is not allowed unless explicitly specified.
        /// </summary>
        public bool AllowWhitespace { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextAttribute"/> class.
        /// The default value for <see cref="AllowWhitespace"/> is <c>false</c>, meaning whitespace is not allowed by default.
        /// </summary>
        public TextAttribute()
            : base(int.MaxValue) // Sets the maximum length to the largest possible integer value
        {
            AllowWhitespace = false; // Default behavior is to disallow whitespace
        }

        /// <summary>
        /// Validates whether the specified value is valid based on the string length and optional whitespace rules.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// If <see cref="AllowWhitespace"/> is <c>false</c>, the method checks that the value does not consist solely of whitespace.
        /// The validation also ensures the string length does not exceed the maximum length defined by the base class.
        /// </remarks>
        public override bool IsValid(object value)
        {
            // Check for whitespace when not allowed
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
