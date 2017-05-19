using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Nameless.Framework.Localization {

    public class NullStringLocalizer : IStringLocalizer {
        private static readonly IStringLocalizer _instance = new NullStringLocalizer();

        public static IStringLocalizer Instance {
            get { return _instance; }
        }

        private NullStringLocalizer() {
        }

        public LocalizedString this[string name] {
            get { return new LocalizedString(name, name); }
        }

        public LocalizedString this[string name, params object[] arguments] {
            get { return new LocalizedString(name, name); }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) {
            return Enumerable.Empty<LocalizedString>();
        }

        public IStringLocalizer WithCulture(CultureInfo culture) {
            return Instance;
        }
    }
}