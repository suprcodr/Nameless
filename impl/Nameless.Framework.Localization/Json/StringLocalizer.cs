using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nameless.Framework.Localization.Json {

    /// <summary>
    /// JSON implementation of <see cref="IStringLocalizer"/>.
    /// </summary>
    public sealed class StringLocalizer : IStringLocalizer {

        #region Private Read-Only Fields

        private readonly ConcurrentDictionary<string, Lazy<JObject>> Cache = new ConcurrentDictionary<string, Lazy<JObject>>();
        private readonly string _baseName;
        private readonly string _applicationName;
        private readonly CultureInfo _culture;
        private readonly IEnumerable<string> _resourceFileLocations;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="StringLocalizer"/>
        /// </summary>
        /// <param name="baseName">The base name.</param>
        /// <param name="applicationName">The application name.</param>
        /// <param name="culture">The culture.</param>
        public StringLocalizer(string baseName, string applicationName, CultureInfo culture = null) {
            Prevent.ParameterNull(baseName, nameof(baseName));
            Prevent.ParameterNull(applicationName, nameof(applicationName));

            _baseName = baseName;
            _applicationName = applicationName;
            _culture = culture ?? CultureInfo.CurrentUICulture;

            // Get a list of possible resource file locations.
            _resourceFileLocations = LocalizerUtil.ExpandPath(baseName, applicationName).ToList();
        }

        #endregion Public Constructors

        #region Private Methods

        private JObject GetResourceObject(CultureInfo currentCulture) {
            var cultureSuffix = string.Concat(".", currentCulture.Name);
            cultureSuffix = cultureSuffix != "." ? cultureSuffix : string.Empty;

            var lazyResource = new Lazy<JObject>(() => {
                // First attempt to find a resource file location that exists.
                string resourcePath = null;
                foreach (var resourceFileLocation in _resourceFileLocations) {
                    resourcePath = string.Concat(resourceFileLocation, cultureSuffix, ".json");
                    if (File.Exists(resourcePath)) { break; }
                    resourcePath = null;
                }
                if (resourcePath == null) { return null; }

                // Found a resource file path: attempt to parse it into a JObject.
                try {
                    using (var resourceFileStream = new FileStream(resourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, options: FileOptions.Asynchronous | FileOptions.SequentialScan))
                    using (var resourceTextReader = new JsonTextReader(new StreamReader(resourceFileStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true))) {
                        return JObject.Load(resourceTextReader);
                    }
                } catch { throw; }
            }, LazyThreadSafetyMode.ExecutionAndPublication);

            lazyResource = Cache.GetOrAdd(cultureSuffix, lazyResource);
            return lazyResource.Value;
        }

        private string[] GetCultureSuffixes(CultureInfo currentCulture) {
            // Get culture suffixes (e.g.: { "nl-NL.", "nl.", "" }).
            string[] cultureSuffixes;
            if (currentCulture != null) {
                cultureSuffixes = !currentCulture.IsNeutralCulture
                    ? cultureSuffixes = new[] { currentCulture.Name + ".", currentCulture.Parent.Name + ".", string.Empty }
                    : cultureSuffixes = new[] { currentCulture.Name + ".", string.Empty };
            } else { cultureSuffixes = new[] { string.Empty }; }
            return cultureSuffixes;
        }

        private CultureInfo[] GetCultures(CultureInfo current) {
            return !current.IsNeutralCulture
                ? new[] { current, current.Parent }
                : new[] { current };
        }

        private string GetLocalizedString(string name, CultureInfo culture) {
            Prevent.ParameterNull(name, nameof(name));

            // Attempt to get resource with the given name from the resource object. if not found, try parent
            // resource object until parent begets himself.
            var currentCulture = CultureInfo.CurrentCulture;
            CultureInfo previousCulture = null;
            do {
                var resourceObject = GetResourceObject(currentCulture);
                if (resourceObject != null) {
                    JToken value;
                    if (resourceObject.TryGetValue(name, out value)) {
                        return value.ToString();
                    }
                }

                // Consult parent culture.
                previousCulture = currentCulture;
                currentCulture = currentCulture.Parent;
            } while (previousCulture != currentCulture);
            return null;
        }

        private Lazy<JObject> GetCacheItem(string key) {
            Cache.TryGetValue(key, out Lazy<JObject> result);

            return result;
        }

        #endregion Private Methods

        #region IStringLocalizer Members

        /// <inheritdoc />
        public LocalizedString this[string name] {
            get {
                var value = GetLocalizedString(name, _culture);
                return new LocalizedString(name: name
                    , value: value ?? name
                    , resourceNotFound: value == null);
            }
        }

        /// <inheritdoc />
        public LocalizedString this[string name, params object[] arguments] {
            get {
                var format = GetLocalizedString(name, _culture);
                return new LocalizedString(name: name
                    , value: string.Format(format ?? name, arguments)
                    , resourceNotFound: format == null);
            }
        }

        /// <inheritdoc />
        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures) {
            var cultures = includeAncestorCultures
                ? new[] { _culture, _culture.Parent }
                : new[] { _culture };

            var result = new List<LocalizedString>();
            foreach (var culture in cultures) {
                var resource = GetResourceObject(culture);
                var items = resource.Properties().Select(_ => new LocalizedString(_.Name, _.Value.ToString(), resourceNotFound: _.Value == null));
                result.AddRange(items);
            }
            return result;
        }

        /// <inheritdoc />
        public IStringLocalizer WithCulture(CultureInfo culture) {
            return new StringLocalizer(_baseName, _applicationName, culture);
        }

        #endregion IStringLocalizer Members
    }
}