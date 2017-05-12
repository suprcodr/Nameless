using System;
using Nameless.Skeleton.Framework.EventSourcing.Events;

namespace Nameless.Skeleton.Framework.EventSourcing.Models {

    /// <summary>
    /// Event entry entity.
    /// </summary>
    public class EventEntity {

        #region Public Virtual Properties

        public virtual Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the aggregate Id.
        /// </summary>
        public virtual Guid AggregateID { get; set; }

        /// <summary>
        /// Gets or sets the event version.
        /// </summary>
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets the event time stamp.
        /// </summary>
        public virtual DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public virtual string EventType { get; set; }

        /// <summary>
        /// Gets or sets the event payload.
        /// </summary>
        public virtual byte[] Payload { get; set; }

        #endregion Public Virtual Properties

        #region Public Static Methods

        /// <summary>
        /// Creates a new event entry based on an <see cref="IEvent"/>.
        /// </summary>
        /// <param name="evt">The event.</param>
        /// <returns>An instance of <see cref="EventEntity"/> representing the <see cref="IEvent"/>.</returns>
        public static EventEntity Create(IEvent evt) {
            return new EventEntity {
                ID = Guid.NewGuid(),
                AggregateID = evt.ID,
                Version = evt.Version,
                TimeStamp = evt.TimeStamp,
                EventType = evt.GetType().FullName,
                Payload = SerializerHelper.Serialize(evt)
            };
        }

        #endregion Public Static Methods

        #region Public Virtual Methods

        /// <summary>
        /// Retrieves an instance of <see cref="IEvent"/> from the current <see cref="Payload"/>.
        /// </summary>
        /// <typeparam name="TEvent">Type of the event.</typeparam>
        /// <returns>An instance of <see cref="IEvent"/>.</returns>
        public virtual TEvent GetEventFromPayload<TEvent>() where TEvent : IEvent {
            return SerializerHelper.Deserialize<TEvent>(Payload);
        }

        /// <summary>
        /// Retrieves an instance of <see cref="IEvent"/> from the current <see cref="Payload"/>.
        /// </summary>
        /// <returns>An instance of <see cref="IEvent"/>.</returns>
        public virtual IEvent GetEventFromPayload() {
            return (IEvent)SerializerHelper.Deserialize(Payload, EventType);
        }

        #endregion Public Virtual Methods
    }
}