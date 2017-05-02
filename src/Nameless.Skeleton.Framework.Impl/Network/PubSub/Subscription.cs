﻿using System;
using System.Reflection;

namespace Nameless.Skeleton.Framework.Network.PubSub {

    /// <summary>
    /// Default implementation of <see cref="ISubscription{TMessage}"/>.
    /// </summary>
    /// <typeparam name="TMessage">Type of the message.</typeparam>
    public sealed class Subscription<TMessage> : ISubscription<TMessage>, IDisposable {
        #region Private Read-Only Fields

        private readonly MethodInfo _methodInfo;
        private readonly IPublisherSubscriber _pubSub;
        private readonly WeakReference _targetObject;
        private readonly bool _isStatic;

        #endregion

        #region Private Fields

        private bool _disposed;

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Subscription{TMessage}"/>.
        /// </summary>
        /// <param name="handler">The message handler.</param>
        /// <param name="pubSub">The publisher/subscriber.</param>
        public Subscription(Action<TMessage> handler, IPublisherSubscriber pubSub) {
            Prevent.ParameterNull(handler, nameof(handler));
            Prevent.ParameterNull(pubSub, nameof(pubSub));

            _methodInfo = handler.GetMethodInfo();
            _pubSub = pubSub;
            _targetObject = new WeakReference(handler.Target);
            _isStatic = handler.Target == null;
        }

        #endregion

        #region Destructor

        /// <summary>
        /// Destructor
        /// </summary>
        ~Subscription() {
            Dispose(disposing: false);
        }

        #endregion

        #region Private Methods

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                _pubSub.Unsubscribe(this);
            }

            _disposed = true;
        }

        #endregion

        #region ISubscription Members

        /// <inheritdoc />
        public Action<TMessage> CreateHandler() {
            if (_targetObject.Target != null && _targetObject.IsAlive) {
                return (Action<TMessage>)_methodInfo.CreateDelegate(typeof(Action<TMessage>), _targetObject.Target);
            }

            if (_isStatic) {
                return (Action<TMessage>)_methodInfo.CreateDelegate(typeof(Action<TMessage>));
            }

            return null;
        }

        #endregion ISubscription Members

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}