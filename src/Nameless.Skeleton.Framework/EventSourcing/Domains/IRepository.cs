﻿using System;

namespace Nameless.Skeleton.Framework.EventSourcing.Domains {

    /// <summary>
    /// Defines methods/properties/events to implement a repository for event store system.
    /// </summary>
    public interface IRepository {

        #region Public Methods

        /// <summary>
        /// Saves an aggregate.
        /// </summary>
        /// <typeparam name="TAggregate">Type of the aggregate.</typeparam>
        /// <param name="aggregate">The aggregate.</param>
        /// <param name="expectedVersion">Version for the aggregate.</param>
        void Save<TAggregate>(TAggregate aggregate, int? expectedVersion = null) where TAggregate : AggregateRoot;

        /// <summary>
        /// Retrieves an aggregate.
        /// </summary>
        /// <typeparam name="TAggregate">Type of the aggregate.</typeparam>
        /// <param name="aggregateID">The aggregate ID.</param>
        /// <returns>A persisted aggregate.</returns>
        TAggregate Get<TAggregate>(Guid aggregateID) where TAggregate : AggregateRoot;

        #endregion Public Methods
    }
}