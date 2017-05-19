using System;
using System.Linq;
using Nameless.Framework.EventSourcing.Domains;
using Nameless.Framework.EventSourcing.Events;

namespace Nameless.Framework.EventSourcing.Snapshots {

    public class SnapshotRepository : IRepository {

        #region Private Read-Only Fields

        private readonly IAggregateFactory _aggregateFactory;
        private readonly IEventStore _eventStore;
        private readonly IRepository _repository;
        private readonly ISnapshotStore _snapshotStore;
        private readonly ISnapshotStrategy _snapshotStrategy;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public SnapshotRepository(IAggregateFactory aggregateFactory, IEventStore eventStore, IRepository repository, ISnapshotStore snapshotStore, ISnapshotStrategy snapshotStrategy) {
            Prevent.ParameterNull(aggregateFactory, nameof(aggregateFactory));
            Prevent.ParameterNull(eventStore, nameof(eventStore));
            Prevent.ParameterNull(repository, nameof(repository));
            Prevent.ParameterNull(snapshotStore, nameof(snapshotStore));
            Prevent.ParameterNull(snapshotStrategy, nameof(snapshotStrategy));

            _aggregateFactory = aggregateFactory;
            _eventStore = eventStore;
            _repository = repository;
            _snapshotStore = snapshotStore;
            _snapshotStrategy = snapshotStrategy;
        }

        #endregion Public Constructors

        #region Private Methods

        private int TryRestoreAggregateFromSnapshot<TAggregate>(Guid aggregateID, TAggregate aggregate) where TAggregate : AggregateRoot {
            var version = -1;
            if (!_snapshotStrategy.IsSnapshotable(typeof(TAggregate))) { return version; }
            var snapshot = _snapshotStore.Get(aggregateID);
            if (snapshot == null) { return version; }
            aggregate.AsDynamic().Restore(snapshot);
            version = snapshot.Version;
            return version;
        }

        private void TryMakeSnapshot(AggregateRoot aggregate) {
            if (!_snapshotStrategy.ShouldMakeSnapshot(aggregate)) { return; }
            var snapshot = aggregate.AsDynamic().GetSnapshot();
            snapshot.Version = aggregate.Version + aggregate.GetUncommittedChanges().Length;
            _snapshotStore.Save(snapshot);
        }

        #endregion Private Methods

        #region IRepository Members

        public TAggregate Get<TAggregate>(Guid aggregateID) where TAggregate : AggregateRoot {
            var aggregate = _aggregateFactory.Create<TAggregate>();
            var snapshotVersion = TryRestoreAggregateFromSnapshot(aggregateID, aggregate);
            if (snapshotVersion == -1) {
                return _repository.Get<TAggregate>(aggregateID);
            }
            var events = _eventStore.Get(aggregateID, snapshotVersion).Where(evt => evt.Version > snapshotVersion);
            aggregate.LoadFromHistory(events);
            return aggregate;
        }

        public void Save<TAggregate>(TAggregate aggregate, int? expectedVersion = default(int?)) where TAggregate : AggregateRoot {
            TryMakeSnapshot(aggregate);
            _repository.Save(aggregate, expectedVersion);
        }

        #endregion IRepository Members
    }
}