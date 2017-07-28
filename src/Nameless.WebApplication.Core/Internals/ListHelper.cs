using System.Collections.Generic;

namespace Nameless.WebApplication.Core {

    internal static class ListHelper {

        #region Internal Static Methods

        internal static IList<T> Empty<T>() {
            return new List<T>();
        }

        internal static IList<T> Create<T>(IEnumerable<T> collection) {
            return new List<T>(collection);
        }

        #endregion Internal Static Methods
    }
}