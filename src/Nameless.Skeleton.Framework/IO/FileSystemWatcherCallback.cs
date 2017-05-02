namespace Nameless.Skeleton.IO {

    /// <summary>
    /// File watcher delegate.
    /// </summary>
    /// <param name="filePath">The path to the file being watched.</param>
    /// <param name="reason">The reason of the callback.</param>
    public delegate void FileSystemWatcherCallback(string filePath, FileSystemWatcherCallbackReason reason);
}