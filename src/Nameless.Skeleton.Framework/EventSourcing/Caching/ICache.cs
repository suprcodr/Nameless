using System;
using Nameless.Skeleton.Framework.EventSourcing.Domains;

namespace Nameless.Skeleton.Framework.EventSourcing.Caching {

    /// <summary>
    /// Defines methods/properties/events to implement a cache tracker.
    /// </summary>
    public interface ICache {

        #region Methods

        /// <summary>
        /// Checks if entry is tracked.
        /// </summary>
        /// <param name="id">The cache entry ID.</param>
        /// <returns><c>true</c> if is tracked; otherwise, <c>false</c>.</returns>
        bool IsTracked(Guid id);

        /// <summary>
        /// Sets an aggregate into the cache.
        /// </summary>
        /// <param name="id">The entry ID.</param>
        /// <param name="aggregate">The aggregate.</param>
        void Set(Guid id, AggregateRoot aggregate);

        /// <summary>
        /// Retrieves an aggregate.
        /// </summary>
        /// <param name="id">The entry ID.</param>
        /// <returns>The aggregate.</returns>
        AggregateRoot Get(Guid id);

        /// <summary>
        /// Removes an entry.
        /// </summary>
        /// <param name="id">The entry ID.</param>
        void Remove(Guid id);

        /// <summary>
        /// Registers a callback, that will be called when an entry is evicted from the cache.
        /// </summary>
        /// <param name="callback">The callback action.</param>
        void RegisterEvictionCallback(Action<Guid> callback);

        #endregion Methods
    }
}