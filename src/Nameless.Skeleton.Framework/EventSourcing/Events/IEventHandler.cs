using Nameless.Skeleton.Framework.EventSourcing.Messaging;

namespace Nameless.Skeleton.Framework.EventSourcing.Events {

    /// <summary>
    /// Defines methods/properties/events to implement an event handler.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event.</typeparam>
    public interface IEventHandler<in TEvent> : IHandle<TEvent> where TEvent : IEvent { }
}