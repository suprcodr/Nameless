using System.Xml.Linq;

namespace Nameless.Skeleton {

    /// <summary>
    /// Extension methods for <see cref="XElement"/>.
    /// </summary>
	public static class XElementExtension {

        #region Public Static Methods

        /// <summary>
        /// Verifies if the attribute (specified by <paramref name="attributeName"/>) is present in the <see cref="XElement"/>.
        /// </summary>
        /// <param name="source">The source <see cref="XElement"/>.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <returns><c>true</c> if is present, otherwise, <c>false</c>.</returns>
        public static bool HasAttribute(this XElement source, string attributeName) {
            Prevent.ParameterNullOrWhiteSpace(attributeName, nameof(attributeName));

            if (source == null) { return false; }

            return source.Attribute(attributeName) != null;
        }

        #endregion Public Static Methods
    }
}