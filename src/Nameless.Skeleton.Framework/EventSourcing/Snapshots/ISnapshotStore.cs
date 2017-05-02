using System;

namespace Nameless.Skeleton.Framework.EventSourcing.Snapshots {

    public interface ISnapshotStore {

        #region Methods

        Snapshot Get(Guid id);

        void Save(Snapshot snapshot);

        #endregion Methods
    }
}