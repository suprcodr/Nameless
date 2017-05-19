using System.Collections.Generic;
using System.IO;

namespace Nameless {

    /// <summary>
    /// Interface for resource manager.
    /// </summary>
    public interface IResourceManager {

        #region Methods

        /// <summary>
        /// Retrieves the names of the resources.
        /// </summary>
        /// <param name="filter">A filter.</param>
        /// <param name="useRegex">Use regular expression.</param>
        /// <returns>A collection with the names of the resources.</returns>
        IEnumerable<string> GetResourceNames(string filter = null, bool useRegex = false);

        /// <summary>
        /// Retrieves a string value from the resource manager.
        /// </summary>
        /// <param name="resourceName">The key.</param>
        /// <returns>The string representation.</returns>
        string GetString(string resourceName);

        /// <summary>
        /// Retrieves a <see cref="Stream"/> from the resource manager.
        /// </summary>
        /// <param name="resourceName">The key.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream GetStream(string resourceName);

        #endregion Methods
    }
}