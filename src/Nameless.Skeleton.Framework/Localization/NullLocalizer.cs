using System.Globalization;

namespace Nameless.Skeleton.Framework.Localization {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="Localizer"/>
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public static class NullLocalizer {

        #region Private Static Read-Only Fields

        private static readonly Localizer _instance = (format, args) => new LocalizableString((args != null && args.Length > 0) ? string.Format(CultureInfo.InvariantCulture, format, args) : format);

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the static instance of <see cref="Localizer"/>.
        /// </summary>
        public static Localizer Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties
    }
}