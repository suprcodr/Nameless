using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;

namespace Nameless.Skeleton.Framework.Localization.Json {

    /// <summary>
    /// JSON implementation of <see cref="IStringLocalizerFactory"/>.
    /// </summary>
    public sealed class StringLocalizerFactory : IStringLocalizerFactory {

        #region Private Static Read-Only Fields

        private static readonly string[] KnownViewExtensions = new[] { ".cshtml" };
        private static readonly ConcurrentDictionary<string, IStringLocalizer> Cache = new ConcurrentDictionary<string, IStringLocalizer>();

        #endregion Private Static Read-Only Fields

        #region Private Read-Only Fields

        private readonly IHostingEnvironment _environment;

        #endregion Private Read-Only Fields

        #region Private Fields

        private string _resourcesRelativePath;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="StringLocalizerFactory"/>.
        /// </summary>
        /// <param name="environment">The host environment.</param>
        /// <param name="options">The localization options.</param>
        public StringLocalizerFactory(IHostingEnvironment environment, LocalizationOptions options) {
            Prevent.ParameterNull(environment, nameof(environment));
            Prevent.ParameterNull(options, nameof(options));

            _environment = environment;
            _resourcesRelativePath = options.ResourcesPath ?? string.Empty;

            if (!string.IsNullOrEmpty(_resourcesRelativePath)) {
                _resourcesRelativePath = string.Concat(_resourcesRelativePath
                    .Replace(Path.AltDirectorySeparatorChar, '.')
                    .Replace(Path.DirectorySeparatorChar, '.'), ".");
            }
        }

        #endregion Public Constructors

        #region IStringLocalizerFactory Members

        /// <inheritdoc />
        public IStringLocalizer Create(Type resourceSource) {
            Prevent.ParameterNull(resourceSource, nameof(resourceSource));

            var typeInfo = resourceSource.GetTypeInfo();

            // Re-root the base name if a resources path is set.
            var resourceBaseName = !string.IsNullOrEmpty(_resourcesRelativePath)
                ? string.Concat(_environment.ApplicationName, ".", _resourcesRelativePath, typeInfo.FullName.TrimPrefix(_environment.ApplicationName + "."))
                : typeInfo.FullName;

            return Cache.GetOrAdd(resourceBaseName, key => new StringLocalizer(key, _environment.ApplicationName));
        }

        /// <inheritdoc />
        public IStringLocalizer Create(string baseName, string location) {
            Prevent.ParameterNull(baseName, nameof(baseName));

            location = location ?? _environment.ApplicationName;

            // Re-root base name if a resources path is set and strip the cshtml part.
            var resourceBaseName = string.Concat(location, ".", _resourcesRelativePath, baseName.TrimPrefix(location + "."));

            var viewExtension = KnownViewExtensions.FirstOrDefault(resourceBaseName.EndsWith);
            if (viewExtension != null) {
                resourceBaseName = resourceBaseName.Substring(startIndex: 0, length: resourceBaseName.Length - viewExtension.Length);
            }

            return Cache.GetOrAdd(resourceBaseName, key => new StringLocalizer(key, _environment.ApplicationName));
        }

        #endregion IStringLocalizerFactory Members
    }
}