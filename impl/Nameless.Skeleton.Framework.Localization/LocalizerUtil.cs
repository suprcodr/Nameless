﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nameless.Skeleton.Framework.Localization {

    /// <summary>
    /// Localizer utilities
    /// </summary>
    public static class LocalizerUtil {

        #region Public Static Methods

        /// <summary>
        /// Expand the specified path.
        /// </summary>
        /// <param name="name">The path name.</param>
        /// <param name="baseName">The path base name.</param>
        /// <returns>A collection of paths.</returns>
        public static IEnumerable<string> ExpandPath(string name, string baseName) {
            Prevent.ParameterNull(name, nameof(name));
            Prevent.ParameterNull(baseName, nameof(baseName));

            return InnerExpandPath(name, baseName);
        }

        #endregion Public Static Methods

        #region Private Static Methods

        private static IEnumerable<string> InnerExpandPath(string name, string baseName) {
            var expansion = new StringBuilder();

            // Start replacing periods, starting at the beginning.
            var components = name.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (var first = 0; first < components.Length; first++) {
                for (var second = 0; second < components.Length; second++) {
                    expansion.Append(components[second]).Append(second < first ? Path.DirectorySeparatorChar : '.');
                }
                // Remove trailing period.
                yield return expansion.Remove(expansion.Length - 1, 1).ToString();
                expansion.Clear();
            }

            // Do the same with the name where baseName prefix is removed.
            var nameWithoutPrefix = name.TrimPrefix(baseName);
            if (nameWithoutPrefix != string.Empty && nameWithoutPrefix != name) {
                nameWithoutPrefix = nameWithoutPrefix.Substring(1);
                var componentsWithoutPrefix = nameWithoutPrefix.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                for (var first = 0; first < componentsWithoutPrefix.Length; first++) {
                    for (var second = 0; second < componentsWithoutPrefix.Length; second++) {
                        expansion.Append(componentsWithoutPrefix[second]).Append(second < first ? Path.DirectorySeparatorChar : '.');
                    }
                    // Remove trailing period.
                    yield return expansion.Remove(expansion.Length - 1, 1).ToString();
                    expansion.Clear();
                }
            }
        }

        #endregion Private Static Methods
    }
}