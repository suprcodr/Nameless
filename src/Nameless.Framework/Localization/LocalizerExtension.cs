using System.Linq;

namespace Nameless.Framework.Localization {

    /// <summary>
    /// Extension methods for <see cref="Localizer"/>.
    /// </summary>
    public static class LocalizerExtension {

        #region Public Static Methods

        /// <summary>
        /// Gets the plural text.
        /// </summary>
        /// <param name="source">The source <see cref="Localizer"/>.</param>
        /// <param name="textSingular">The singular text value.</param>
        /// <param name="textPlural">The plural text value.</param>
        /// <param name="count">The count of objects for the text.</param>
        /// <param name="args">The text arguments.</param>
        /// <returns>The pluralized version of the text.</returns>
        public static LocalizableString Plural(this Localizer source, string textSingular, string textPlural, int count, params object[] args) {
            if (source == null) { return LocalizableString.Empty; }

            return source(count == 1 ? textSingular : textPlural, new object[] { count }.Concat(args).ToArray());
        }

        /// <summary>
        /// Gets the plural text.
        /// </summary>
        /// <param name="source">The source <see cref="Localizer"/>.</param>
        /// <param name="textNone">The non-plural version of the text.</param>
        /// <param name="textSingular">The singular text value.</param>
        /// <param name="textPlural">The plural text value.</param>
        /// <param name="count">The count of objects for the text.</param>
        /// <param name="args">The text arguments.</param>
        /// <returns>The pluralized version of the text.</returns>
		public static LocalizableString Plural(this Localizer source, string textNone, string textSingular, string textPlural, int count, params object[] args) {
            if (source == null) { return LocalizableString.Empty; }

            switch (count) {
                case 0: return source(textNone, new object[] { count }.Concat(args).ToArray());
                case 1: return source(textSingular, new object[] { count }.Concat(args).ToArray());
                default: return source(textPlural, new object[] { count }.Concat(args).ToArray());
            }
        }

        #endregion Public Static Methods
    }
}