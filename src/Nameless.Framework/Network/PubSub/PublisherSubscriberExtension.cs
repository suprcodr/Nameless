using System.Threading.Tasks;

namespace Nameless.Framework.Network.PubSub {

    /// <summary>
    /// Extension methods for <see cref="IPublisherSubscriber"/>
    /// </summary>
    public static class PublisherSubscriberExtension {

        #region Public Static Methods

        /// <summary>
        /// Publishes a data notification for a specific topic, asynchronous.
        /// </summary>
        /// <param name="source">The publisher-subscriber instance.</param>
        /// <typeparam name="TMessage">Type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>A <see cref="Task"/> for the process.</returns>
        public static Task PublishAsync<TMessage>(this IPublisherSubscriber source, TMessage message) {
            if (source == null) { return Task.CompletedTask; }

            return Task.Run(() => source.Publish(message));
        }

        #endregion Public Static Methods
    }
}