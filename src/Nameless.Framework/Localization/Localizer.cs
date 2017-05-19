using System;
using System.Collections.Generic;
using System.Text;

namespace Nameless.Framework.Localization
{
    /// <summary>
    /// Localizes some text based on the current culture
    /// </summary>
    /// <param name="text">The text format to localize</param>
    /// <param name="args">The arguments used in the text format.</param>
    /// <returns>An HTML-encoded localized string</returns>
    public delegate LocalizableString Localizer(string text, params object[] args);
}
