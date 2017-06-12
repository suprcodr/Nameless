using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Network.PubSub {

    /// <summary>
    /// Extension methods for <see cref="IPublisherSubscriber"/>
    /// </summary>
    public static class PublisherSubscriberExtension {

        #region Public Static Methods

        /// <summary>
        /// Publishes a data notification for a specific topic, asynchronously.
        /// </summary>
        /// <param name="source">The publisher-subscriber instance.</param>
        /// <typeparam name="TMessage">Type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>A <see cref="Task"/> representing the publication process.</returns>
        public static Task PublishAsync<TMessage>(this IPublisherSubscriber source, TMessage message) {
            if (source == null) { return Task.CompletedTask; }

            return source.PublishAsync(message, CancellationToken.None);
        }

        /// <summary>
        /// Publishes a data notification for a specific topic, synchronously.
        /// </summary>
        /// <param name="source">The publisher-subscriber instance.</param>
        /// <typeparam name="TMessage">Type of the message.</typeparam>
        /// <param name="message">The message.</param>
        public static async void Publish<TMessage>(this IPublisherSubscriber source, TMessage message) {
            if (source == null) { return; }

            await source.PublishAsync(message, CancellationToken.None);
        }

        #endregion Public Static Methods
    }
}