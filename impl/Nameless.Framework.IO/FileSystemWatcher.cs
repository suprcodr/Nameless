using System;
using System.Collections.Generic;
using System.IO;

namespace Nameless.IO {

    /// <summary>
    /// Default implementation of <see cref="IFileSystemWatcher"/>.
    /// </summary>
    public class FileSystemWatcher : IFileSystemWatcher, IDisposable {

        #region Private Fields

        private EventHandler _eventHandler;
        private System.IO.FileSystemWatcher _watcher;
        private IDictionary<int, object[]> _watcherEvents = new Dictionary<int, object[]>();
        private bool _disposed;

        #endregion Private Fields

        #region Internal Events

        internal event EventHandler Disposed {
            add { _eventHandler += value; }
            remove { _eventHandler -= value; }
        }

        #endregion Internal Events

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="FileWatcher"/>
        /// </summary>
        /// <param name="filePath">The file to watch.</param>
        public FileSystemWatcher(string filePath) {
            Prevent.ParameterNullOrWhiteSpace(filePath, nameof(filePath));

            FilePath = filePath;

            Initialize();
        }

        #endregion Public Constructors

        #region Destructor

        ~FileSystemWatcher() {
            Dispose(false);
        }

        #endregion Destructor

        #region Private Methods

        private void Initialize() {
            _watcher = new System.IO.FileSystemWatcher {
                Path = Path.GetDirectoryName(FilePath),
                NotifyFilter = NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                Filter = Path.GetFileName(FilePath),
                EnableRaisingEvents = false
            };
        }

        private void OnDispose() {
            _eventHandler?.Invoke(this, EventArgs.Empty);
        }

        private void OnFileDelete(object sender, FileSystemEventArgs args) {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (_disposed) { return; }

            if (disposing) {
                OnDispose();

                if (_watcher != null) {
                    _watcher.EnableRaisingEvents = false;
                    _watcher.Dispose();
                }
            }

            _eventHandler = null;
            _watcher = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IFileWatcher Members

        /// <inheritdoc />
        public string FilePath { get; private set; }

        /// <inheritdoc />
        public void RegisterCallback(FileSystemWatcherCallback callback) {
            if (callback == null) { return; }

            var hash = callback.GetHashCode();
            if (_watcherEvents.ContainsKey(hash)) { return; }

            _watcher.EnableRaisingEvents = false;

            _watcherEvents.Add(hash, new object[] {
                new FileSystemEventHandler((sender, evt) => callback(evt.FullPath, FileSystemWatcherCallbackReason.Modified)),
                new FileSystemEventHandler((sender, evt) => callback(evt.FullPath, FileSystemWatcherCallbackReason.Deleted)),
                new RenamedEventHandler((sender, evt) => callback(evt.FullPath, FileSystemWatcherCallbackReason.Renamed))
            });

            var entry = _watcherEvents[hash];
            _watcher.Changed += (FileSystemEventHandler)entry[0];
            _watcher.Deleted += (FileSystemEventHandler)entry[1];
            _watcher.Deleted += OnFileDelete; // Notify if the file was deleted. If true, then dispose the watcher.
            _watcher.Renamed += (RenamedEventHandler)entry[2];

            _watcher.EnableRaisingEvents = true;
        }

        /// <inheritdoc />
        public void UnregisterCallback(FileSystemWatcherCallback callback) {
            if (callback == null) { return; }

            var hash = callback.GetHashCode();
            if (!_watcherEvents.ContainsKey(hash)) { return; }

            _watcher.EnableRaisingEvents = false;

            var entry = _watcherEvents[hash];
            _watcher.Changed -= (FileSystemEventHandler)entry[0];
            _watcher.Deleted -= (FileSystemEventHandler)entry[1];
            _watcher.Renamed -= (RenamedEventHandler)entry[2];

            entry[0] = null;
            entry[1] = null;
            entry[2] = null;
            _watcherEvents.Remove(hash);

            _watcher.EnableRaisingEvents = true;
        }

        /// <inheritdoc />
        public void Destroy() {
            Dispose(true);
        }

        #endregion IFileWatcher Members

        #region IDisposable Members

        /// <inheritdoc />
        void IDisposable.Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}