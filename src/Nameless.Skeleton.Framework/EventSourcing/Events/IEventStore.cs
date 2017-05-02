using System;
using System.Collections.Generic;

namespace Nameless.Skeleton.Framework.EventSourcing.Events {

    /// <summary>
    /// Defines methods/properties/events to persists or restore an event.
    /// </summary>
    public interface IEventStore {

        #region Methods

        /// <summary>
        /// Saves the event.
        /// </summary>
        /// <param name="evts">The collection event.</param>
        void Save(params IEvent[] evts);

        /// <summary>
        /// Retrieves all events for the aggregate ID, from specified version.
        /// </summary>
        /// <param name="aggregateID">The aggregate ID.</param>
        /// <param name="fromVersion">Specified version to find.</param>
        /// <returns>A collection of <see cref="IEvent"/>.</returns>
        IEnumerable<IEvent> Get(Guid aggregateID, int fromVersion);

        #endregion Methods
    }
}