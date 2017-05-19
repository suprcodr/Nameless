using System;
using System.Collections.Generic;

namespace Nameless.Framework.EventSourcing.Domains {

    /// <summary>
    /// Default implementation of <see cref="ISession"/>
    /// </summary>
    public class Session : ISession {

        #region Private Read-Only Fields

        private readonly IRepository _repository;
        private readonly IDictionary<Guid, AggregateDescriptor> _cache = new Dictionary<Guid, AggregateDescriptor>();

        #endregion Private Read-Only Fields

        #region Public Constructors

        public Session(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region Private Methods

        private bool IsTracked(Guid id) {
            return _cache.ContainsKey(id);
        }

        #endregion Private Methods

        #region ISession Members

        public void Add<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot {
            if (_cache.ContainsKey(aggregate.ID) && _cache[aggregate.ID].Aggregate == aggregate) {
                throw new ConcurrencyException(aggregate.ID);
            }

            _cache.Add(aggregate.ID, new AggregateDescriptor { Aggregate = aggregate, Version = aggregate.Version });
        }

        public void Commit() {
            _cache.Values.Each(_ => _repository.Save(_.Aggregate, _.Version));
            _cache.Clear();
        }

        public TAggregate Get<TAggregate>(Guid id, int? expectedVersion = default(int?)) where TAggregate : AggregateRoot {
            TAggregate aggregate;

            if (_cache.ContainsKey(id)) {
                aggregate = (TAggregate)_cache[id].Aggregate;
                if (expectedVersion.HasValue && aggregate.Version != expectedVersion.Value) {
                    throw new ConcurrencyException(aggregate.ID);
                }
            }
            
            aggregate = _repository.Get<TAggregate>(id);
            if (expectedVersion.HasValue && aggregate.Version != expectedVersion.Value) {
                throw new ConcurrencyException(aggregate.ID);
            }

            Add(aggregate);

            return aggregate;
        }

        #endregion ISession Members
    }
}