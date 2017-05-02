using System;

namespace Nameless.Skeleton.Framework.EventSourcing.Snapshots {

    /// <summary>
    /// Abstract clas to define a snapshot object.
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