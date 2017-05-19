using System;
using System.Collections.Generic;
using System.Linq;
using Nameless.Framework.Data;
using Nameless.Framework.EventSourcing.Models;

namespace Nameless.Framework.EventSourcing.Events {

    /// <summary>
    /// Default implementation of <see cref="IEventStore"/>
    /// </summary>
    public class EventStore : IEventStore {

        #region Private Read-Only Fields

        private readonly IEventPublisher _publisher;
        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="EventStore"/>
        /// </summary>
        public EventStore(IEventPublisher publisher, IRepository repository) {
            Prevent.ParameterNull(publisher, nameof(publisher));
            Prevent.ParameterNull(repository, nameof(repository));

            _publisher = publisher;
            _repository = repository;
        }

        #endregion Public Constructors

        #region IEventStore Members

        /// <inheritdoc />
        public IEnumerable<IEvent> Get(Guid aggregateID, int fromVersion) {
            return _repository
                .Query<EventEntity>()
                .Where(_ => _.ID == aggregateID && _.Version >= fromVersion)
                .Select(_ => _.GetEventFromPayload());
        }

        /// <inheritdoc />
        public void Save(params IEvent[] evts) {
            foreach (var evt in evts) {
                var entity = EventEntity.Create(evt);
                _repository.Save(entity);
                _publisher.Publish(evt);
            }
        }

        #endregion IEventStore Members
    }
}