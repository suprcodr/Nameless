using System;
using Nameless.Framework.EventSourcing.Domains;

namespace Nameless.Framework.EventSourcing.Caching {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="ICache"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullCache : ICache {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="ICache"/>.
        /// </summary>
        public static readonly ICache Instance = new NullCache();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullCache() {
        }

        #endregion Private Constructors

        #region ICache Members

        /// <inheritdoc />
        public bool IsTracked(Guid id) {
            return false;
        }

        /// <inheritdoc />
        public void Set(Guid id, AggregateRoot aggregate) {
        }

        /// <inheritdoc />
        public AggregateRoot Get(Guid id) {
            return null;
        }

        /// <inheritdoc />
        public void Remove(Guid id) {
        }

        /// <inheritdoc />
        public void RegisterEvictionCallback(Action<Guid> callback) {
        }

        #endregion ICache Members
    }
}