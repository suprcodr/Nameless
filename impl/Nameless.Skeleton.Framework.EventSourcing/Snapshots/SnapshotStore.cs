using System;
using Nameless.Skeleton.Framework.Data;
using Nameless.Skeleton.Framework.EventSourcing.Models;

namespace Nameless.Skeleton.Framework.EventSourcing.Snapshots {

    /// <summary>
    /// Default implementation of <see cref="ISnapshotStore"/>
    /// </summary>
    public class SnapshotStore : ISnapshotStore {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="SnapshotStore"/>
        /// </summary>
        public SnapshotStore(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region ISnapshotStore Members

        public Snapshot Get(Guid id) {
            return _repository.FindOne<SnapshotEntity>(id).GetSnapshotFromPayload();
        }

        public void Save(Snapshot snapshot) {
            var entity = SnapshotEntity.Create(snapshot);

            _repository.Save(entity);
        }

        #endregion ISnapshotStore Members
    }
}