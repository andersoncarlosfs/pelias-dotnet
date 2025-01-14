﻿using Pelias.NET.Model.Objects.Pelias.GeographicInformationSystems.Measurements.Measures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pelias.NET.Model.Objects.Pelias.Converters
{
    /// <summary>
    /// Custom JSON converter for serializing and deserializing instances of the <see cref="Angle"/> class.
    /// This converter handles the conversion of <see cref="Angle"/> objects to and from JSON format,
    /// specifically dealing with angle values represented as numeric degrees.
    /// </summary>
    public class DegreesConverter : JsonConverter<Angle>
    {
        private readonly bool _throwOnError;

        /// <summary>
        /// Initializes a new instance of the <see cref="DegreesConverter"/> class.
        /// </summary>
        /// <param name="throwOnError">
        /// A flag indicating whether exceptions should be thrown when the conversion fails.
        /// If set to <c>true</c>, an exception will be thrown in case of conversion failure.
        /// If set to <c>false</c>, <see cref="null"/> will be returned on failure.
        /// </param>
        public DegreesConverter(bool throwOnError = false)
        {
            _throwOnError = throwOnError;
        }

        /// <summary>
        /// Converts JSON data to an <see cref="Angle"/> object.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> instance used to read the JSON content.
        /// This provides a way to read the JSON structure and extract values.
        /// </param>
        /// <param name="typeToConvert">
        /// The type that this converter is responsible for converting to. In this case, it is <see cref="Angle"/>.
        /// </param>
        /// <param name="options">
        /// A <see cref="JsonSerializerOptions"/> instance containing options that affect the deserialization process.
        /// </param>
        /// <returns>
        /// An <see cref="Angle"/> object corresponding to the JSON data, or <c>null</c> if the conversion fails
        /// and <paramref name="throwOnError"/> is <c>false</c>. If <paramref name="throwOnError"/> is <c>true</c>,
        /// an exception will be thrown if conversion fails.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown if conversion fails and <paramref name="throwOnError"/> is <c>true</c>.
        /// </exception>
        public override Angle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Attempt to read a double value from the JSON, which represents the angle in degrees.
            if (_throwOnError)
            {
                // If throwOnError is true, force conversion and throw if it fails.
                return new Angle(reader.GetDouble());
            }

            // If reading the double fails, return null.
            return reader.TryGetDouble(out var degrees) ? new Angle(degrees) : null;
        }

        /// <summary>
        /// Converts an <see cref="Angle"/> object to its JSON representation.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="Utf8JsonWriter"/> instance used to write the JSON content.
        /// This is responsible for serializing the <see cref="Angle"/> object to its JSON form.
        /// </param>
        /// <param name="value">
        /// The <see cref="Angle"/> object that is being serialized to JSON.
        /// </param>
        /// <param name="options">
        /// A <see cref="JsonSerializerOptions"/> instance that contains options affecting serialization.
        /// </param>
        public override void Write(Utf8JsonWriter writer, Angle value, JsonSerializerOptions options)
        {
            // Serialize the Degrees property of the Angle object as a JSON number.
            writer.WriteNumberValue(value.Degrees);
        }
    }
}
