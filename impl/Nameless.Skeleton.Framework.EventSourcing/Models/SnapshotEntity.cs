using System;
using Nameless.Skeleton.Framework.EventSourcing.Snapshots;

namespace Nameless.Skeleton.Framework.EventSourcing.Models {

    /// <summary>
    /// Snapshot entry entity.
    /// </summary>
    public class SnapshotEntity {

        #region Public Virtual Properties

        public virtual Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the aggregate ID.
        /// </summary>
        public virtual Guid AggregateID { get; set; }

        /// <summary>
        /// Gets or sets the snapshot version.
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets the snapshot type.
        /// </summary>
        public virtual string SnapshotType { get; set; }

        /// <summary>
        /// Gets or sets the snapshot payload.
        /// </summary>
        public virtual byte[] Payload { get; set; }

        #endregion Public Virtual Properties

        #region Public Static Methods

        /// <summary>
        /// Creates an instance of <see cref="SnapshotEntity"/> from a <see cref="Snapshots.Snapshot"/>.
        /// </summary>
        /// <param name="snapshot">The <see cref="Snapshots.Snapshot"/> instance.</param>
        /// <returns>An instance of <see cref="SnapshotEntity"/> representing the <see cref="Snapshots.Snapshot"/>.</returns>
        public static SnapshotEntity Create(Snapshots.Snapshot snapshot) {
            return new SnapshotEntity {
                ID = Guid.NewGuid(),
                AggregateID = snapshot.ID,
                Version = snapshot.Version,
                SnapshotType = snapshot.GetType().FullName,
                Payload = SerializerHelper.Serialize(snapshot)
            };
        }

        #endregion Public Static Methods

        #region Public Virtual Methods

        /// <summary>
        /// Retrieves an instance of <see cref="Snapshots.Snapshot"/> from the <see cref="Payload"/>.
        /// </summary>
        /// <typeparam name="TSnapshot">Type of the snapshot.</typeparam>
        /// <returns>An instance of <see cref="Snapshots.Snapshot"/>.</returns>
        public virtual TSnapshot GetSnapshotFromPayload<TSnapshot>() where TSnapshot : Snapshots.Snapshot {
            return SerializerHelper.Deserialize<TSnapshot>(Payload);
        }

        /// <summary>
        /// Retrieves an instance of <see cref="Snapshot"/> from the <see cref="Payload"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Snapshot"/>.</returns>
        public virtual Snapshot GetSnapshotFromPayload() {
            return (Snapshot)SerializerHelper.Deserialize(Payload, SnapshotType);
        }

        #endregion Public Virtual Methods
    }
}