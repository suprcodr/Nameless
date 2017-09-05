using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Nameless.Helpers {

    /// <summary>
    /// XML helper methods.
    ///
    /// Singleton Pattern implementation for XmlHelper. (see: https://en.wikipedia.org/wiki/Singleton_pattern)
    /// </summary>
    public sealed class XmlHelper {

        #region Private Static Read-Only Fields

        private static readonly XmlHelper _instance = new XmlHelper();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of XmlHelper.
        /// </summary>
        public static XmlHelper Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static XmlHelper() {
        }

        #endregion Static Constructors

        #region Private Constructors

        private XmlHelper() {
        }

        #endregion Private Constructors

        #region Public Methods

        public void Serialize(Stream stream, object obj, IDictionary<string, string> namespaces = null) {
            Prevent.ParameterNull(stream, nameof(stream));
            Prevent.ParameterNull(obj, nameof(obj));

            var xmlSerializer = new XmlSerializer(obj.GetType());
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            if (namespaces != null && namespaces.Count > 0) {
                foreach (var kvp in namespaces) {
                    xmlSerializerNamespaces.Add(kvp.Key, kvp.Value);
                }
            } else { xmlSerializerNamespaces.Add(string.Empty, string.Empty); }

            xmlSerializer.Serialize(stream, obj, xmlSerializerNamespaces);
        }

        public string Serialize(object obj, IDictionary<string, string> namespaces = null) {
            Prevent.ParameterNull(obj, nameof(obj));

            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream)) {
                Serialize(memoryStream, obj, namespaces);
                memoryStream.Seek(offset: 0, loc: SeekOrigin.Begin);

                return streamReader.ReadToEnd();
            }
        }

        public T Deserialize<T>(Stream stream) {
            Prevent.ParameterNull(stream, nameof(stream));

            using (var streamReader = new StreamReader(stream)) {
                return Deserialize<T>(streamReader.ReadToEnd());
            }
        }

        public T Deserialize<T>(string value) {
            Prevent.ParameterNull(value, nameof(value));

            using (var stringReader = new StringReader(value)) {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        #endregion Public Methods
    }
}