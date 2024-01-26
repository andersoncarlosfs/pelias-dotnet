using System.Runtime.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Extensions
{
    public static class EnumExtensions
    {
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

            throw new InvalidOperationException($"The attribute '{nameof(EnumMemberAttribute)}' was not found for the enum value '{value}' of the '{typeof(T)}'.");
        }
    }
}
