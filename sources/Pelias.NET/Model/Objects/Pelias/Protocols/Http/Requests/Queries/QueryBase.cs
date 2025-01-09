using Pelias.NET.Model.Interfaces.Protocols.Http;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Pelias.NET.Model.Objects.Pelias.Protocols.Http.Requests.Queries
{

    public abstract class QueryBase : IQuery
    {
        /// <summary>
        /// Validates the necessary attributes for URL encoding.
        /// </summary>
        private List<ValidationResult> Validate()
        {
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true);

            return validationResults;
        }

        /// <summary>
        /// Creates a collection of properties for URL encoding.
        /// </summary>
        private IEnumerable<PropertyInfo> GetProperties()
        {
            foreach (var property in GetType().GetProperties())
            {
                if (!property.GetCustomAttributes(typeof(JsonIgnoreAttribute)).Any())
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        /// Creates a collection of parameters with their respective values for URL encoding.
        /// </summary>
        public NameValueCollection ToNameValueCollection()
        {
            var exceptions = Validate();

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions.Select(validation => new ValidationException(validation.ErrorMessage)));
            }

            var nameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var property in GetProperties())
            {
                var name = property.Name;

                foreach (var attribute in (JsonPropertyNameAttribute[])property.GetCustomAttributes(typeof(JsonPropertyNameAttribute)))
                {
                    name = attribute.Name;
                }

                var value = property.GetValue(this, null);

                nameValueCollection[name] = value?.ToString();

                foreach (var attribute in (JsonConverterAttribute[])property.GetCustomAttributes(typeof(JsonConverterAttribute)))
                {
                    var options = new JsonSerializerOptions();

                    options.Converters.Add((JsonConverter)Activator.CreateInstance(attribute.ConverterType));

                    nameValueCollection[name] = JsonSerializer.Serialize(value, options);
                }
            }

            return nameValueCollection;
        }
    }
}
