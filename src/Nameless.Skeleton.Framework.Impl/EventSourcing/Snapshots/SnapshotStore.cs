using System;
using System.Data;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.EventSourcing.Models;
using SQL = Nameless.Skeleton.Framework.EventSourcing.Resources.Resource;

namespace Nameless.Skeleton.Framework.EventSourcing.Snapshots {

    /// <summary>
    /// Default implementation of <see cref="ISnapshotStore"/>
    /// </summary>
    public class SnapshotStore : ISnapshotStore {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="SnapshotStore"/>
        /// </summary>
        public SnapshotStore(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region Private Methods

        private Snapshot Map(IDataReader reader) {
            return new SnapshotEntity {
                SnapshotType = reader.GetStringOrDefault("snapshot_type"),
                Payload = reader.GetBlobOrDefault("payload"),
            }.GetSnapshotFromPayload();
        }

        #endregion Private Methods

        #region ISnapshotStore Members

        public Snapshot Get(Guid id) {
            return _database.ExecuteReaderSingle(SQL.GetSnapshot, Map, CommandType.Text, new[] {
                Parameter.CreateInputParameter("AggregateID", id, DbType.Guid)
            });
        }

        public void Save(Snapshot snapshot) {
            var entity = SnapshotEntity.Create(snapshot);

            _database.ExecuteNonQuery(SQL.CreateSnapshot, CommandType.Text, new[] {
                Parameter.CreateInputParameter(nameof(SnapshotEntity.ID), entity.ID, DbType.Guid),
                Parameter.CreateInputParameter(nameof(SnapshotEntity.AggregateID), entity.AggregateID, DbType.Guid),
                Parameter.CreateInputParameter(nameof(SnapshotEntity.Version), entity.Version, DbType.Int32),
                Parameter.CreateInputParameter(nameof(SnapshotEntity.SnapshotType), entity.SnapshotType),
                Parameter.CreateInputParameter(nameof(SnapshotEntity.Payload), entity.Payload, DbType.Binary)
            });
        }

        #endregion ISnapshotStore Members
    }
}