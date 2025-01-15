using Pelias.NET.Model.Interfaces.Protocols.Http;
using Pelias.NET.Model.Resources;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries
{
    /// <summary>
    /// Represents the base class for a query in the Pelias API.
    /// This class provides the functionality to validate the object's properties and
    /// serialize them into a query string for HTTP requests.
    /// </summary>
    public abstract class QueryBase : IQuery
    {
        /// <summary>
        /// Validates the necessary attributes for URL encoding, including required fields.
        /// </summary>
        /// <returns>A list of validation results.</returns>
        /// <remarks>
        /// This method checks the object for any validation attributes and collects validation
        /// errors if any are present. It uses the <see cref="Validator.TryValidateObject"/> method.
        /// </remarks>
        public List<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();

            // Validate the object based on its data annotations
            Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);

            return validationResults;
        }

        /// <summary>
        /// Creates a collection of parameters with their respective values for URL encoding.
        /// Converts the properties of this object to a name-value collection for URL query string generation.
        /// </summary>
        /// <returns>A <see cref="NameValueCollection"/> containing the query string parameters.</returns>
        /// <exception cref="AggregateException">
        /// Thrown if any validation errors occur during the validation of the object.
        /// </exception>
        /// <remarks>
        /// This method checks for the <see cref="JsonIgnoreAttribute"/> to exclude properties
        /// from the URL string when necessary. It also handles the <see cref="JsonPropertyNameAttribute"/>
        /// to map properties to the correct names and applies any custom converters through
        /// the <see cref="JsonConverterAttribute"/>.
        /// </remarks>
        public NameValueCollection ToNameValueCollection()
        {
            var exceptions = Validate();

            // If there are validation errors, throw an exception with all the validation messages.
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions.Select(validation => new ValidationException(validation.ErrorMessage)));
            }

            var nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            // Iterate over each property in the current class
            foreach (var property in GetType().GetProperties())
            {
                var type = property.PropertyType;
                var name = property.Name;
                var value = property.GetValue(this);

                // Handle the JsonIgnore attribute conditions
                foreach (JsonIgnoreAttribute attribute in property.GetCustomAttributes(typeof(JsonIgnoreAttribute)))
                {
                    name = attribute.Condition switch
                    {
                        JsonIgnoreCondition.Never => name, // Always include this property
                        JsonIgnoreCondition.Always => null, // Always exclude this property
                        JsonIgnoreCondition.WhenWritingDefault =>
                            type.IsValueType && type.GetConstructor(Type.EmptyTypes) != null && Activator.CreateInstance(type) == value ? null : name, // Exclude if the property is default
                        JsonIgnoreCondition.WhenWritingNull => value == null ? null : name, // Exclude if the property is null
                        _ => throw new NotImplementedException(string.Format(ExceptionsResources.NotImplementedException_MissingPropertyAttributeValue,
                                  attribute.Condition, nameof(attribute.Condition), attribute.GetType)) // Handle unimplemented conditions
                    };
                }

                // If the property is ignored, skip it
                if (name == null)
                {
                    continue;
                }

                // Handle the JsonPropertyName attribute to map property names
                foreach (var attribute in (JsonPropertyNameAttribute[])property.GetCustomAttributes(typeof(JsonPropertyNameAttribute)))
                {
                    name = attribute.Name;
                }

                var options = new JsonSerializerOptions();

                // Handle any custom JSON converters for the property
                foreach (var attribute in (JsonConverterAttribute[])property.GetCustomAttributes(typeof(JsonConverterAttribute)))
                {
                    options.Converters.Add((JsonConverter)Activator.CreateInstance(attribute.ConverterType));
                }

                // Serialize the property value and add it to the nameValueCollection
                nameValueCollection[name] = JsonSerializer.Serialize(value, options);
            }

            return nameValueCollection;
        }
    }
}
