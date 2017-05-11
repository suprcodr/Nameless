using System;
using System.Collections.Generic;
using System.Data;
using Nameless.Skeleton.Framework.Data.Sql.Ado;
using Nameless.Skeleton.Framework.EventSourcing.Models;
using SQL = Nameless.Skeleton.Framework.EventSourcing.Resources.Resource;

namespace Nameless.Skeleton.Framework.EventSourcing.Events {

    /// <summary>
    /// Default implementation of <see cref="IEventStore"/>
    /// </summary>
    public class EventStore : IEventStore {

        #region Private Read-Only Fields

        private readonly IDatabase _database;
        private readonly IEventPublisher _publisher;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        public EventStore(IDatabase database, IEventPublisher publisher) {
            Prevent.ParameterNull(database, nameof(database));
            Prevent.ParameterNull(publisher, nameof(publisher));

            _database = database;
            _publisher = publisher;
        }

        #endregion Public Constructors

        #region Private Methods

        private IEvent Map(IDataReader reader) {
            return new EventEntity {
                EventType = reader.GetStringOrDefault("event_type"),
                Payload = reader.GetBlobOrDefault("payload"),
            }.GetEventFromPayload();
        }

        #endregion Private Methods

        #region IEventStore Members

        /// <inheritdoc />
        public IEnumerable<IEvent> Get(Guid aggregateID, int fromVersion) {
            return _database.ExecuteReader(SQL.ListEvents, Map, CommandType.Text, new[] {
                Parameter.CreateInputParameter("AggregateID", aggregateID, DbType.Guid),
                Parameter.CreateInputParameter("Version", fromVersion)
            });
        }

        /// <inheritdoc />
        public void Save(params IEvent[] evts) {
            foreach (var evt in evts) {
                var entity = EventEntity.Create(evt);

                _database.ExecuteNonQuery(SQL.CreateEvent, CommandType.Text, new[] {
                    Parameter.CreateInputParameter(nameof(EventEntity.ID), entity.ID, DbType.Guid),
                    Parameter.CreateInputParameter(nameof(EventEntity.AggregateID), entity.AggregateID, DbType.Guid),
                    Parameter.CreateInputParameter(nameof(EventEntity.Version), entity.Version, DbType.Int32),
                    Parameter.CreateInputParameter(nameof(EventEntity.TimeStamp), entity.TimeStamp, DbType.DateTimeOffset),
                    Parameter.CreateInputParameter(nameof(EventEntity.EventType), entity.EventType),
                    Parameter.CreateInputParameter(nameof(EventEntity.Payload), entity.Payload, DbType.Binary)
                });

                _publisher.Publish(evt);
            }
        }

        #endregion IEventStore Members
    }
}