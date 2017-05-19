using System;
using Nameless.Framework.EventSourcing.Messaging;

namespace Nameless.Framework.EventSourcing.Events {

    /// <summary>
    /// Defines methods/properties/events for an event/message.
    /// </summary>
    public interface IEvent : IMessage {

        #region Properties

        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the event version.
        /// </summary>
        int Version { get; set; }

        /// <summary>
        /// Gets or sets the event time stamp.
        /// </summary>
        DateTimeOffset TimeStamp { get; set; }

        #endregion Properties
    }
}