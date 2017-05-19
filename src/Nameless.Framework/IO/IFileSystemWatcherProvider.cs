namespace Nameless.IO {

    /// <summary>
    /// Defines methods/properties/events to implement a file watcher provider.
    /// </summary>
    public interface IFileSystemWatcherProvider {

        #region Methods

        IFileSystemWatcher Watch(string filePath);

        #endregion Methods
    }
}