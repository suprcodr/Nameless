using System;
using System.Collections.Concurrent;

namespace Nameless.IO {

    /// <summary>
    /// Default implementation of <see cref="IFileSystemWatcherProvider"/>
    /// </summary>
    public class FileSystemWatcherProvider : IFileSystemWatcherProvider, IDisposable {

        #region Private Static Fields

        private static ConcurrentDictionary<string, IFileSystemWatcher> _cache = new ConcurrentDictionary<string, IFileSystemWatcher>();

        #endregion Private Static Fields

        #region Private Fields

        private bool _disposed;

        #endregion Private Fields

        #region Destructor

        ~FileSystemWatcherProvider() {
            Dispose(false);
        }

        #endregion Destructor

        #region Private Methods

        private void OnFileSystemWatcherDispose(object sender, EventArgs e) {
            _cache.TryRemove(((IFileSystemWatcher)sender).FilePath, out IFileSystemWatcher dummy);
        }

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                lock (_cache) {
                    _cache.Values.Each(_ => {
                        var watcher = (FileSystemWatcher)_;
                        watcher.Disposed -= OnFileSystemWatcherDispose;
                        watcher.TryDispose();
                    });
                    _cache.Clear();
                }
            }

            _cache = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IFileSystemWatcherProvider Members

        /// <inheritdoc />
        public IFileSystemWatcher Watch(string filePath) {
            return _cache.GetOrAdd(filePath, _ => {
                var fileWatcher = new FileSystemWatcher(_);
                fileWatcher.Disposed += OnFileSystemWatcherDispose;
                return fileWatcher;
            });
        }

        #endregion IFileSystemWatcherProvider Members

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}