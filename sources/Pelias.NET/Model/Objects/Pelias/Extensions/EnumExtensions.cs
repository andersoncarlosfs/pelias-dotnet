using Pelias.NET.Model.Resources;
using System.Runtime.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Extensions
{
    /// <summary>
    /// Provides extension methods for enums, specifically for retrieving the value of the <see cref="EnumMemberAttribute"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the string value of the <see cref="EnumMemberAttribute"/> associated with the specified enum value.
        /// If no <see cref="EnumMemberAttribute"/> is found, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value for which the <see cref="EnumMemberAttribute"/> value should be retrieved.</param>
        /// <returns>The string value of the <see cref="EnumMemberAttribute"/> for the specified enum value.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <see cref="EnumMemberAttribute"/> is not found for the specified enum value.
        /// </exception>
        public static string GetEnumMemberValue<T>(this T value)
            where T : Enum
        {
            foreach (var member in typeof(T).GetMember(value.ToString()))
            {
                var attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(member, typeof(EnumMemberAttribute));

                if (attribute != null)
                {
                    return attribute.Value;
                }
            }

            throw new InvalidOperationException(
                string.Format(ExceptionsResources.InvalidOperationException_MissingAttribute, 
                    nameof(EnumMemberAttribute), 
                    value, 
                    typeof(T)));
        }
    }
}
