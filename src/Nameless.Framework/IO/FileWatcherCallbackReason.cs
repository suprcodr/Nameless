namespace Nameless.IO {

    /// <summary>
    /// File watcher callback reasons.
    /// </summary>
    public enum FileSystemWatcherCallbackReason {

        /// <summary>
        /// No reason.
        /// </summary>
        None = 0,

        /// <summary>
        /// File renamed.
        /// </summary>
        Renamed = 1,

        /// <summary>
        /// File deleted.
        /// </summary>
        Deleted = 2,

        /// <summary>
        /// File modified.
        /// </summary>
        Modified = 3
    }
}