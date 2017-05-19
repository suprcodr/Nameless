namespace Nameless.IO {

    /// <summary>
    /// Defines methods/properties/events to implement a file watcher.
    /// </summary>
    public interface IFileSystemWatcher {

        #region Properties

        /// <summary>
        /// Gets the file path being watched.
        /// </summary>
        string FilePath { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Registers a callback to inform about file changes.
        /// </summary>
        /// <param name="callback">The callback.</param>
        void RegisterCallback(FileSystemWatcherCallback callback);

        /// <summary>
        /// Unregister the callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        void UnregisterCallback(FileSystemWatcherCallback callback);

        /// <summary>
        /// Destroy the watcher.
        /// </summary>
        void Destroy();

        #endregion Methods
    }
}