using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Nameless.Skeleton {

    /// <summary>
    /// Implementation of <see cref="IResourceManager"/> to look up for embedded resources in the specified assembly.
    /// </summary>
    public class ResourceManager : IResourceManager {

        #region Private Read-Only Fields

        private readonly Assembly _assembly;
        private readonly string[] _manifestResourceNames;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ResourceManager"/>
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public ResourceManager(Assembly assembly) {
            Prevent.ParameterNull(assembly, nameof(assembly));

            _assembly = assembly;
            _manifestResourceNames = assembly.GetManifestResourceNames();
        }

        #endregion Public Constructors

        #region Public Static Methods

        /// <summary>
        /// Creates a new instance of the <see cref="ResourceManager"/>
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A new instance of <see cref="ResourceManager"/>.</returns>
        public static IResourceManager Create(Assembly assembly) {
            return new ResourceManager(assembly);
        }

        #endregion Public Static Methods

        #region IResourceManager Members

        /// <inheritdoc />
        public IEnumerable<string> GetResourceNames(string filter = null, bool useRegex = false) {
            if (!string.IsNullOrWhiteSpace(filter) && useRegex) {
                var regex = new Regex(filter);

                return _manifestResourceNames.Where(_ => regex.IsMatch(_));
            }

            if (!string.IsNullOrWhiteSpace(filter) && !useRegex) {
                return _manifestResourceNames.Where(_ => _.IndexOf(filter) > 0);
            }

            return _manifestResourceNames;
        }

        /// <inheritdoc />
        public Stream GetStream(string resourceName) {
            return _assembly.GetManifestResourceStream(resourceName);
        }

        /// <inheritdoc />
        public string GetString(string resourceName) {
            using (var stream = new StreamReader(GetStream(resourceName), Encoding.UTF8)) {
                return stream.ReadToEnd();
            }
        }

        #endregion IResourceManager Members
    }
}