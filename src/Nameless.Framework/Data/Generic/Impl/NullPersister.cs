using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    /// <summary>
    /// Null Object Pattern implementation for IPersister. (see: https://en.wikipedia.org/wiki/Null_Object_pattern)
    /// </summary>
    public sealed class NullPersister : IPersister {

        #region Private Static Read-Only Fields

        private static readonly IPersister _instance = new NullPersister();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of NullPersister.
        /// </summary>
        public static IPersister Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static NullPersister() {
        }

        #endregion Static Constructors

        #region Private Constructors

        // Prevents the class from being constructed.
        private NullPersister() {
        }

        #endregion Private Constructors

        #region IPersister Members

        public Task SaveAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return Task.CompletedTask;
        }

        public Task DeleteAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return Task.CompletedTask;
        }

        public Task UpdateAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return Task.CompletedTask;
        }

        #endregion IPersister Members
    }
}