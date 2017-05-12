using System.Collections.Generic;

namespace Nameless.Skeleton.Framework.Localization {

    /// <summary>
    /// Represents a localized string.
    /// </summary>
    public sealed class LocalizableString {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Empty localizable string instance.
        /// </summary>
        public static readonly LocalizableString Empty = new LocalizableString(string.Empty, string.Empty, string.Empty, new object[0]);

        #endregion Public Static Read-Only Fields

        #region Public Properties

        /// <summary>
        /// Gets the localization scope.
        /// </summary>
        public string Scope { get; }

        /// <summary>
        /// Gets the original text.
        /// </summary>
        public string TextHint { get; }

        /// <summary>
        /// Gets the localized text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the arguments, if any.
        /// </summary>
        public IEnumerable<object> Args { get; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="LocalizableString"/>.
        /// </summary>
        /// <param name="languageNeutral">The neutral text.</param>
        public LocalizableString(string languageNeutral) {
            TextHint = languageNeutral;
            Text = languageNeutral;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LocalizableString"/>.
        /// </summary>
        /// <param name="text">The localized text.</param>
        /// <param name="scope">The localization scope.</param>
        /// <param name="textHint">The original text.</param>
        /// <param name="args">The arguments.</param>
        public LocalizableString(string scope, string textHint, string text, object[] args) {
            Scope = scope;
            TextHint = textHint;
            Text = text;
            Args = args;
        }

        #endregion Public Constructors

        #region Public Static Methods

        /// <summary>
        /// Retrieves the localized text or default value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The default value if text does not exists.</returns>
        public static LocalizableString TextOrDefault(string text, LocalizableString defaultValue) {
            return !string.IsNullOrEmpty(text) ? new LocalizableString(text) : defaultValue;
        }

        #endregion Public Static Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override string ToString() {
            return Text;
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            var hashCode = 0;
            if (Text != null) {
                hashCode ^= Text.GetHashCode();
            }
            return hashCode;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) {
            if (obj == null || obj.GetType() != GetType()) {
                return false;
            }
            return string.Equals(Text, ((LocalizableString)obj).Text);
        }

        #endregion Public Override Methods
    }
}