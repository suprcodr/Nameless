using System.Xml.Linq;
using System.Xml.XPath;

namespace Nameless {

    /// <summary>
    /// Extension methods for <see cref="XContainer"/>.
    /// </summary>
	public static class XContainerExtension {

        #region Public Static Methods

        /// <summary>
        /// Verifies if the <paramref name="elementName"/> is present into the <paramref name="source"/> <see cref="XContainer"/>.
        /// </summary>
        /// <param name="source">The source <see cref="XContainer"/>.</param>
        /// <param name="elementName">The element name.</param>
        /// <returns><c>true</c> if exists, otherwise, <c>false</c>.</returns>
        public static bool HasElement(this XContainer source, string elementName) {
            Prevent.ParameterNullOrWhiteSpace(elementName, nameof(elementName));

            if (source == null) { return false; }

            return source.Element(elementName) != null;
        }

        /// <summary>
        /// Verifies if the <paramref name="elementName"/> with attribute (specified by <paramref name="attributeName"/>) and attribute value (specified by <paramref name="attributeValue"/>) is present into the <paramref name="source"/> <see cref="XContainer"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="elementName"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static bool HasElement(this XContainer source, string elementName, string attributeName, string attributeValue) {
            Prevent.ParameterNullOrWhiteSpace(elementName, nameof(elementName));
            Prevent.ParameterNullOrWhiteSpace(attributeName, nameof(attributeName));
            Prevent.ParameterNullOrWhiteSpace(attributeValue, nameof(attributeValue));

            if (source == null) { return false; }

            const string expressionPattern = "./{0}[@{1}='{2}']";

            var expression = string.Format(expressionPattern
                , elementName
                , attributeName
                , attributeValue);

            return source.XPathSelectElement(expression) != null;
        }

        #endregion Public Static Methods
    }
}