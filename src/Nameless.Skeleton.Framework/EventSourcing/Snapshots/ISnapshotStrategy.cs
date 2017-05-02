using System;
using Nameless.Skeleton.Framework.EventSourcing.Domains;

namespace Nameless.Skeleton.Framework.EventSourcing.Snapshots {

    /// <summary>
    /// Defines methods/properties/events to implement a snapshot strategy.
    /// </summary>
    public interface ISnapshotStrategy {

        #region Methods

        /// <summary>
        /// Checks if the aggregate root should be snapshotted.
        /// </summary>
        /// <param name="aggregate">The aggregate root.</param>
        /// <returns><c>true</c> if should be snapshotted; otherwise, <c>false</c>.</returns>
        bool ShouldMakeSnapshot(AggregateRoot aggregate);

        /// <summary>
        /// Checks if the aggregate type can be snapshotable.
        /// </summary>
        /// <param name="aggregateType">The aggregate type.</param>
        /// <returns><c>true</c> if is snapshotable; otherwise, <c>false</c>.</returns>
        bool IsSnapshotable(Type aggregateType);

        #endregion Methods
    }
}