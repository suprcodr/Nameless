using System;

namespace Nameless.Framework.EventSourcing.Snapshots {

    /// <summary>
    /// Abstract class for snapshot object.
    /// </summary>
    public abstract class Snapshot {

        #region Public Properties

        /// <summary>
        /// Gets or sets the snapshot ID.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the snapshot version.
        /// </summary>
        public int Version { get; set; }

        #endregion Public Properties
    }
}