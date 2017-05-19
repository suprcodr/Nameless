namespace Nameless.Framework.Localization {

    /// <summary>
    /// Defines methods for text localized messages.
    /// </summary>
	public interface IText {

        #region Methods

        /// <summary>
        /// Retrieves a localized string by its text hint.
        /// </summary>
        /// <param name="textHint">The text hint.</param>
        /// <param name="args">The text arguments.</param>
        /// <returns>The localized string.</returns>
        LocalizableString Get(string textHint, params object[] args);

        #endregion Methods
    }
}