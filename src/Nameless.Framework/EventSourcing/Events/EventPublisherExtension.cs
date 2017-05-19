using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.EventSourcing.Events {

    /// <summary>
    /// Extension methods for <see cref="IEventPublisher"/>.
    /// </summary>
    public static class EventPublisherExtension {

        #region Public Static Methods

        /// <summary>
        /// Publishes an event.
        /// </summary>
        /// <param name="source">The source <see cref="IEventPublisher"/>.</param>
        /// <param name="eventType">The event type.</param>
        /// <param name="evt">The event.</param>
        public static void Publish(this IEventPublisher source, Type eventType, IEvent evt) {
            if (source == null) { return; }

            Prevent.ParameterNull(eventType, nameof(eventType));
            Prevent.ParameterTypeNotAssignableFrom(typeof(IEvent), eventType);
            Prevent.ParameterNull(evt, nameof(evt));

            typeof(IEventPublisher)
                .GetMethod(nameof(IEventPublisher.Publish))
                .MakeGenericMethod(new[] { eventType })
                .Invoke(source, new object[] { evt });
        }

        /// <summary>
        /// Publishes an event async.
        /// </summary>
        /// <typeparam name="TEvent">Type of the event.</typeparam>
        /// <param name="source">The source <see cref="IEventPublisher"/>.</param>
        /// <param name="evt">The event.</param>
        /// <returns>The <see cref="Task"/> referes to the event dispatching.</returns>
        public static Task PublishAsync<TEvent>(this IEventPublisher source, TEvent evt)
            where TEvent : IEvent {
            return PublishAsync(source, evt, CancellationToken.None);
        }

        /// <summary>
        /// Publishes an event async.
        /// </summary>
        /// <typeparam name="TEvent">Type of the event.</typeparam>
        /// <param name="source">The source <see cref="IEventPublisher"/>.</param>
        /// <param name="evt">The event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> referes to the event dispatching.</returns>
        public static Task PublishAsync<TEvent>(this IEventPublisher source, TEvent evt, CancellationToken cancellationToken)
            where TEvent : IEvent {
            if (source == null) { return Task.CompletedTask; }

            Prevent.ParameterNull(evt, nameof(evt));

            return Task.Run(() => source.Publish(evt), cancellationToken);
        }

        /// <summary>
        /// Publishes an event async.
        /// </summary>
        /// <param name="source">The source <see cref="IEventPublisher"/>.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="evt">The event.</param>
        /// <returns>The <see cref="Task"/> referes to the event dispatching.</returns>
        public static Task PublishAsync(this IEventPublisher source, Type eventType, IEvent evt) {
            return PublishAsync(source, eventType, evt, CancellationToken.None);
        }

        /// <summary>
        /// Publishes an event async.
        /// </summary>
        /// <param name="source">The source <see cref="IEventPublisher"/>.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="evt">The event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/> referes to the event dispatching.</returns>
        public static Task PublishAsync(this IEventPublisher source, Type eventType, IEvent evt, CancellationToken cancellationToken) {
            return Task.Run(() => Publish(source, eventType, evt), cancellationToken);
        }

        #endregion Public Static Methods
    }
}