using System;
using Microsoft.Extensions.Caching.Memory;
using Nameless.Skeleton.Framework.EventSourcing.Domains;

namespace Nameless.Skeleton.Framework.EventSourcing.Caching {

    /// <summary>
    /// Default implementation of <see cref="ICache"/>.
    /// </summary>
    public sealed class Cache : ICache, IDisposable {

        #region Private Fields

        private MemoryCacheEntryOptions _options;
        private IMemoryCache _memoryCache;
        private bool _disposed;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Cache"/>.
        /// </summary>
        public Cache()
            : this(new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(15) }) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cache"/>.
        /// </summary>
        /// <param name="options">The memory cache entry options.</param>
        public Cache(MemoryCacheEntryOptions options) {
            Prevent.ParameterNull(options, nameof(options));

            _options = options;
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        #endregion Public Constructors

        #region Destructor

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Cache() {
            Dispose(disposing: false);
        }

        #endregion Destructor

        #region Private Methods

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_memoryCache != null) {
                    _memoryCache.Dispose();
                }
            }
            _options = null;
            _memoryCache = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region ICache Members

        /// <inheritdoc />
        public AggregateRoot Get(Guid id) {
            return _memoryCache.Get<AggregateRoot>(id);
        }

        /// <inheritdoc />
        public bool IsTracked(Guid id) {
            return _memoryCache.TryGetValue(id, out object dummy);
        }

        /// <inheritdoc />
        public void RegisterEvictionCallback(Action<Guid> action) {
            _options.RegisterPostEvictionCallback((key, value, reason, state) => action((Guid)key));
        }

        /// <inheritdoc />
        public void Remove(Guid id) {
            _memoryCache.Remove(id);
        }

        /// <inheritdoc />
        public void Set(Guid id, AggregateRoot aggregate) {
            _memoryCache.Set(id, aggregate, _options);
        }

        #endregion ICache Members

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}