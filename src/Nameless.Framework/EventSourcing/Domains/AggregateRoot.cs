using System;
using System.Collections.Generic;
using System.Linq;
using Nameless.Framework.EventSourcing.Events;

namespace Nameless.Framework.EventSourcing.Domains {

    /// <summary>
    /// Abstract aggregate root.
    /// </summary>
    public abstract class AggregateRoot {

        #region Private Read-Only Fields

        private readonly IList<IEvent> _changes = new List<IEvent>();

        #endregion Private Read-Only Fields

        #region Public Properties

        /// <summary>
        /// Gets the aggregate ID.
        /// </summary>
        public Guid ID { get; protected set; }

        /// <summary>
        /// Gets the aggregate version.
        /// </summary>
        public int Version { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Retrieves a collection of uncommited changes.
        /// </summary>
        /// <returns></returns>
        public IEvent[] GetUncommittedChanges() {
            lock (_changes) {
                return _changes.ToArray();
            }
        }

        /// <summary>
        /// Flushes the aggregate changes.
        /// </summary>
        /// <returns></returns>
        public IEvent[] FlushUncommitedChanges() {
            lock (_changes) {
                var changes = _changes.ToArray();
                var idx = 0;
                foreach (var evt in changes) {
                    if (evt.ID == Guid.Empty && ID == Guid.Empty) {
                        throw new AggregateOrEventMissingIDException(GetType(), evt.GetType());
                    }
                    if (evt.ID == Guid.Empty) {
                        evt.ID = ID;
                    }
                    idx++;
                    evt.Version = Version + idx;
                    evt.TimeStamp = DateTimeOffset.UtcNow;
                }
                Version = Version + _changes.Count;
                _changes.Clear();
                return changes;
            }
        }

        public void LoadFromHistory(IEnumerable<IEvent> history) {
            foreach (var evt in history) {
                if (evt.Version != Version + 1) {
                    throw new EventsOutOfOrderException(evt.ID);
                }
                ApplyChange(evt, false);
            }
        }

        public bool Equals(AggregateRoot obj) {
            return obj != null && obj.ID == ID;
        }

        #endregion Public Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as AggregateRoot);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return ID.GetHashCode();
        }

        #endregion Public Override Methods

        #region Protected Methods

        protected void ApplyChange(IEvent evt) {
            ApplyChange(evt, true);
        }

        #endregion Protected Methods

        #region Private Methods

        private void ApplyChange(IEvent evt, bool isNew) {
            lock (_changes) {
                this.AsDynamic().Apply(evt);
                if (!isNew) {
                    ID = evt.ID;
                    Version++;
                } else { _changes.Add(evt); }
            }
        }

        #endregion Private Methods
    }
}