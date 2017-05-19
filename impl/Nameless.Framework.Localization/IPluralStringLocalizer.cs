﻿using Microsoft.Extensions.Localization;

namespace Nameless.Framework.Localization {

    /// <summary>
    /// Plural string localizer
    /// </summary>
    public interface IPluralStringLocalizer {

        #region Methods

        /// <summary>
        /// Gets the string resource with the given name and formatted with the supplied arguments.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="pluralName">The name of the plural string resource.</param>
        /// <param name="count">The number of items represented in the plural form.</param>
        /// <param name="arguments">The values to format the string with.</param>
        /// <returns>The formatted string resource as a <see cref="LocalizedString"/>.</returns>
        LocalizedString this[string name, string pluralName, int count, params object[] arguments] { get; }

        #endregion Methods
    }

    /// <summary>
    /// Generic interface of <see cref="IPluralStringLocalizer"/>.
    /// </summary>
    /// <typeparam name="T">Type scope.</typeparam>
    public interface IPluralStringLocalizer<T> : IPluralStringLocalizer {
    }
}