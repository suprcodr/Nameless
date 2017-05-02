namespace Nameless.Skeleton.Framework.EventSourcing.Events {

    /// <summary>
    /// Defines methods/properties/events to dispatch a specific event.
    /// </summary>
    public interface IEventPublisher {

        #region Methods

        /// <summary>
        /// Dispatches an event.
        /// </summary>
        /// <typeparam name="TEvent">Type of the event.</typeparam>
        /// <param name="evt">The event.</param>
        void Publish<TEvent>(TEvent evt) where TEvent : IEvent;

        #endregion Methods
    }
}