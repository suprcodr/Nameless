using System;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace Nameless.Framework.Caching {

    /// <summary>
    /// Default implementation of <see cref="ICache"/>.
    /// </summary>
    public sealed class InMemoryCache : ICache, IDisposable {

        #region Private Fields

        private IMemoryCache _cache;
        private bool _disposed;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="InMemoryCache"/>.
        /// </summary>
        public InMemoryCache() {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        #endregion Public Constructors

        #region Destructor

        /// <summary>
        /// Destructor.
        /// </summary>
        ~InMemoryCache() {
            Dispose(disposing: false);
        }

        #endregion Destructor

        #region Private Methods

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_cache != null) {
                    _cache.Dispose();
                }
            }
            _cache = null;
            _disposed = true;
        }

        private void SetEvictionCallback(MemoryCacheEntryOptions options, Action<string> callback) {
            if (callback != null) {
                options.RegisterPostEvictionCallback((cacheKey, value, reason, state) => callback((string)cacheKey));
            }
        }

        private void SetCacheDependency(MemoryCacheEntryOptions options, CacheDependency dependency) {
            if (dependency is FileCacheDependency) {
                SetFileCacheDependency(options, dependency as FileCacheDependency);
            }

            if (dependency is TimeCacheDependency) {
                SetTimeCacheDependency(options, dependency as TimeCacheDependency);
            }
        }

        private void SetFileCacheDependency(MemoryCacheEntryOptions options, FileCacheDependency dependency) {
            var fileInfo = new FileInfo(dependency.FilePath);
            var fileProvider = new PhysicalFileProvider(fileInfo.DirectoryName);

            // TODO: Checks if this token (fileProvider) needs to be disposed after cache entry eviction.
            options.AddExpirationToken(fileProvider.Watch(fileInfo.Name));
        }

        private void SetTimeCacheDependency(MemoryCacheEntryOptions options, TimeCacheDependency dependency) {
            switch (dependency.Policy) {
                case CacheItemPolicy.AbsoluteExpiration:
                    options.SetAbsoluteExpiration(dependency.Time);
                    break;

                case CacheItemPolicy.SlidingExpiration:
                    options.SetSlidingExpiration(dependency.Time);
                    break;
            }
        }

        #endregion Private Methods

        #region ICache Members

        /// <inheritdoc />
        public object Get(string key) {
            return _cache.Get(key);
        }

        /// <inheritdoc />
        public bool IsTracked(string key) {
            return _cache.TryGetValue(key, out object dummy);
        }

        /// <inheritdoc />
        public void Remove(string key) {
            _cache.Remove(key);
        }

        /// <inheritdoc />
        public void Set(string key, object obj, Action<string> evictionCallback = null, CacheDependency dependency = null) {
            var options = new MemoryCacheEntryOptions();

            SetEvictionCallback(options, evictionCallback);
            SetCacheDependency(options, dependency);

            _cache.Set(key, obj, options);
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