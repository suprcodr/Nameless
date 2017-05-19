using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Nameless {

    /// <summary>
    /// Extension methods for <see cref="object"/>
    /// </summary>
    public static class ObjectExtension {

        #region Public Static Methods

        /// <summary>
        /// Tries dispose the object, if it implements the <see cref="IDisposable"/> interface.
        /// </summary>
        /// <param name="source">The source <see cref="object"/>.</param>
        /// <returns><c>true</c> if disposed, otherise, <c>false</c>.</returns>
        public static bool TryDispose(this object source) {
            var disposable = source as IDisposable;

            if (disposable == null) { return false; }

            disposable.Dispose();

            return true;
        }

        /// <summary>
		/// Converts the <paramref name="source"/> <see cref="object"/> to a XML <see cref="string"/> representation.
		/// </summary>
		/// <param name="source">The source <see cref="object" />.</param>
		/// <returns>A XML <see cref="string"/>.</returns>
		public static string ToXml(this object source) {
            if (source == null) { return null; }

            return (!source.GetType().IsAnonymous() ?
                ConvertComplexObjectToXml(source) :
                ConvertAnonymousObjectToXml(source).ToString());
        }

        /// <summary>
        /// Converts an anonymous object into a <see cref="Dictionary{String, Object}"/>.
        /// </summary>
        /// <param name="source">The source <see cref="object"/>.</param>
        /// <returns>A <see cref="Dictionary{String, Object}"/>.</returns>
        public static Dictionary<string, object> ToAnonymousDictionary(this object source) {
            if (source == null) { return null; }
            if (!source.GetType().IsAnonymous()) { return null; }

            var result = new Dictionary<string, object>();
            foreach (var property in source.GetType().GetTypeInfo().GetProperties()) {
                result.Add(property.Name, property.GetValue(source, null));
            }

            return result;
        }

        /// <summary>
        /// Converts a struct to a <see cref="Nullable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of the struct</typeparam>
        /// <param name="source">The source value.</param>
        /// <returns>A nullable value.</returns>
        public static T? AsNullable<T>(this T source) where T : struct {
            return new T?(source);
        }

        #endregion Public Static Methods

        #region Private Static Methods

        private static string ConvertComplexObjectToXml(object input) {
            if (input == null) { return string.Empty; }

            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream)) {
                var xmlSerializer = new XmlSerializer(input.GetType());
                xmlSerializer.Serialize(memoryStream, input);
                xmlSerializer = null;

                memoryStream.Seek(0, SeekOrigin.Begin);

                return streamReader.ReadToEnd();
            }
        }

        private static XElement ConvertAnonymousObjectToXml(object input) {
            if (input == null) { return null; }

            return ConvertAnonymousObjectToXml(input, null);
        }

        private static XElement ConvertAnonymousObjectToXml(object input, string element) {
            if (input == null) { return null; }
            if (string.IsNullOrEmpty(element)) { element = "root"; }

            element = XmlConvert.EncodeName(element);
            var result = new XElement(element);

            var type = input.GetType();
            var properties = type.GetTypeInfo().GetProperties();
            var elements = from property in properties
                           let name = XmlConvert.EncodeName(property.Name)
                           let val = property.PropertyType.IsArray ? "array" : property.GetValue(input, null)
                           let value = property.PropertyType.IsArray ? GetArrayElement(property, (Array)property.GetValue(input, null)) : (property.PropertyType.IsSimple() ? new XElement(name, val) : ConvertAnonymousObjectToXml(val, name))
                           where value != null
                           select value;

            result.Add(elements);

            return result;
        }

        private static XElement GetArrayElement(PropertyInfo info, Array input) {
            var name = XmlConvert.EncodeName(info.Name);
            var rootElement = new XElement(name);
            var arrayCount = input.GetLength(0);

            for (var idx = 0; idx < arrayCount; idx++) {
                var value = input.GetValue(idx);
                var childElement = value.GetType().IsSimple() ? new XElement(string.Concat(name, "Child"), value) : ConvertAnonymousObjectToXml(value);
                rootElement.Add(childElement);
            }

            return rootElement;
        }

        #endregion Private Static Methods
    }
}