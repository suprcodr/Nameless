using System.Threading;

namespace Nameless.Framework.Network.PubSub {

    /// <summary>
    /// Extension methods for <see cref="IPublisherSubscriber"/>
    /// </summary>
    public static class PublisherSubscriberExtension {

        #region Public Static Methods

        /// <summary>
        /// Publishes a data notification for a specific topic, synchronously.
        /// </summary>
        /// <param name="source">The publisher-subscriber instance.</param>
        /// <typeparam name="TMessage">Type of the message.</typeparam>
        /// <param name="message">The message.</param>
        public static void Publish<TMessage>(this IPublisherSubscriber source, TMessage message) {
            if (source == null) { return; }

            source.PublishAsync(message, CancellationToken.None).WaitForResult();
        }

        #endregion Public Static Methods
    }
}