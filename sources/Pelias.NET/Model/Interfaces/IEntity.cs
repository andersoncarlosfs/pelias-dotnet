using Pelias.NET.Model.Exceptions;
using Pelias.NET.Model.Resources;
using System.Text.Json;

namespace Pelias.NET.Model.Interfaces
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Compares two JSON elements and returns a list of missing properties in the source compared to the target.
        /// </summary>
        /// <param name="source">The source JSON element.</param>
        /// <param name="target">The target JSON element to compare against.</param>
        /// <param name="parents">The list of parent properties (used for tracking nested properties).</param>
        /// <param name="exceptions">A list to store exceptions during the comparison.</param>
        /// <param name="raiseExceptions">Flag to determine whether to raise exceptions for mismatches (default is false).</param>
        /// <returns>A collection of lists, each representing a path to a missing property in the source element.</returns>
        public static IEnumerable<List<JsonProperty>> GetMissingProperties(JsonElement source, JsonElement target, List<JsonProperty> parents, List<Exception> exceptions, bool raiseExceptions = false)
        {
            // Checking if there is mismatch
            if (!source.ValueKind.Equals(target.ValueKind))
            {
                exceptions.Add(new TypeMismatchException(string.Format(ExceptionsResources.TypeMismatchException_NotEqual_JsonValueKind, target.ValueKind, nameof(target), source.ValueKind, nameof(source))));
            }

            // Checking it the document is an dictionary
            if (source.ValueKind.Equals(JsonValueKind.Object))
            {
                // Processing the dictionary
                foreach (var item in source.EnumerateObject())
                {
                    // Creating the list of entries
                    var keys = new List<JsonProperty>(parents);

                    // Adding the current entry
                    keys.Add(item);

                    // Checking if the document contains the key
                    if (target.TryGetProperty(item.Name, out var value))
                    {
                        // Processing the item
                        foreach (var properties in IEntity.GetMissingProperties(item.Value, value, keys, exceptions, false))
                        {
                            yield return properties;
                        }
                    }
                    else
                    {
                        // Yield the missing entries
                        yield return keys;

                    }
                }
            }

            // Checking it the document is an array
            if (source.ValueKind.Equals(JsonValueKind.Array))
            {
                // Retrieving the enumerator
                using (var enumerator = source.EnumerateArray())
                {
                    // Retrieving the enumerator
                    var subject = target.EnumerateArray();

                    // Processing the array
                    while (enumerator.MoveNext())
                    {
                        // Moving the index
                        if (subject.MoveNext())
                        {
                            // Processing the item
                            foreach (var entry in IEntity.GetMissingProperties(enumerator.Current, subject.Current, parents, exceptions, false))
                            {
                                yield return entry;
                            }
                        }
                        else
                        {
                            exceptions.Add(new CollectionIterationException(string.Format(ExceptionsResources.CollectionIterationException, nameof(target), target.GetArrayLength(), source.GetArrayLength())));
                        }
                    }
                }
            }

            // Checking if there is any exception
            if (raiseExceptions && exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
