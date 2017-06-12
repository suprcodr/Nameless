using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Network.PubSub {

    /// <summary>
    /// Default implementation of <see cref="IPublisherSubscriber"/>.
    /// </summary>
    public sealed class PublisherSubscriber : IPublisherSubscriber {

        #region Private Static Read-Only Fields

        private static readonly object SyncLock = new object();

        #endregion Private Static Read-Only Fields

        #region Private Fields

        private IDictionary<Type, IList> _subscriptions = new Dictionary<Type, IList>();

        #endregion Private Fields

        #region IPublisherSubscriber Members

        /// <inheritdoc />
        public ISubscription<TMessage> Subscribe<TMessage>(Action<TMessage> handler) {
            Prevent.ParameterNull(handler, nameof(handler));

            var messageType = typeof(TMessage);
            var action = new Subscription<TMessage>(handler, this);
            lock (SyncLock) {
                if (!_subscriptions.TryGetValue(messageType, out IList list)) {
                    _subscriptions.Add(messageType, new List<ISubscription<TMessage>> { action });
                } else {
                    _subscriptions[messageType].Add(action);
                }
            }
            return action;
        }

        /// <inheritdoc />
        public bool Unsubscribe<TMessage>(ISubscription<TMessage> subscription) {
            Prevent.ParameterNull(subscription, nameof(subscription));

            var messageType = typeof(TMessage);
            if (_subscriptions.ContainsKey(messageType)) {
                lock (SyncLock) {
                    _subscriptions[messageType].Remove(subscription);
                }
            }
            return true;
        }

        /// <inheritdoc />
        public Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken) {
            Prevent.ParameterNull(message, nameof(message));

            return Task.Run(() => {
                var messageType = typeof(TMessage);
                if (_subscriptions.ContainsKey(messageType)) {
                    lock (SyncLock) {
                        foreach (var subscription in _subscriptions[messageType].OfType<ISubscription<TMessage>>()) {
                            if (cancellationToken.IsCancellationRequested) { break; }
                            var handler = subscription.CreateHandler();
                            if (handler != null) {
                                handler.Invoke(message);
                            }
                        }
                    }
                }
            });
        }

        #endregion IPublisherSubscriber Members
    }
}