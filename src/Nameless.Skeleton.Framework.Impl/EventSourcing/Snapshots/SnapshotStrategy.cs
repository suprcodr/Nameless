using System;
using System.Reflection;
using Nameless.Skeleton.Framework.EventSourcing.Domains;

namespace Nameless.Skeleton.Framework.EventSourcing.Snapshots {

    /// <summary>
    /// Default implementation of <see cref="ISnapshotStrategy"/>
    /// </summary>
    public class SnapshotStrategy : ISnapshotStrategy {

        #region Private Constants

        private const int DefaultSnapshotInterval = 100;

        #endregion Private Constants

        #region Private Fields

        private int _snapshotInterval;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="SnapshotStrategy"/>.
        /// </summary>
        /// <param name="snapshotInterval">Interval between snapshots.</param>
        public SnapshotStrategy(int snapshotInterval = DefaultSnapshotInterval) {
            _snapshotInterval = snapshotInterval > 0 ? snapshotInterval : DefaultSnapshotInterval;
        }

        #endregion Public Constructors

        #region ISnapshotStrategy Members

        /// <inheritdoc />
        public bool IsSnapshotable(Type aggregateType) {
            if (aggregateType.GetTypeInfo().BaseType == null) {
                return false;
            }

            if (aggregateType.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType && aggregateType.GetTypeInfo().BaseType.GetGenericTypeDefinition() == typeof(SnapshotAggregateRoot<>)) {
                return true;
            }

            return IsSnapshotable(aggregateType.GetTypeInfo().BaseType);
        }

        /// <inheritdoc />
        public bool ShouldMakeSnapshot(AggregateRoot aggregate) {
            if (!IsSnapshotable(aggregate.GetType())) { return false; }

            var version = aggregate.Version;

            for (var counter = 0; counter < aggregate.GetUncommittedChanges().Length; counter++) {
                if (++version % _snapshotInterval == 0 && version != 0) {
                    return true;
                }
            }
            return false;
        }

        #endregion ISnapshotStrategy Members
    }
}