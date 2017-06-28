using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Helpers {

    /// <summary>
    /// Asynchronous helper.
    /// </summary>
    public static class AsyncHelper {

        #region Public Static Methods

        /// <summary>
        /// Executes an asynchronous method synchronous.
        /// </summary>
        /// <param name="item">The async method.</param>
        public static void RunSync(Func<Task> item) {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(async _ => {
                try { await item(); }
                finally { synch.EndMessageLoop(); }
            }, null);
            synch.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
        }

        /// <summary>
        /// Executes an asynchronous method synchronous.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="item">The async method.</param>
        public static T RunSync<T>(Func<Task<T>> item) {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            T result = default(T);
            synch.Post(async _ => {
                try { result = await item(); }
                finally { synch.EndMessageLoop(); }
            }, null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            return result;
        }

        #endregion Public Static Methods

        #region Private Inner Classes

        private class ExclusiveSynchronizationContext : SynchronizationContext {

            #region Private Fields

            private bool _done;

            #endregion Private Fields

            #region Private Read-Only Fields

            private readonly AutoResetEvent _workItemsWaiting = new AutoResetEvent(false);
            private readonly Queue<Tuple<SendOrPostCallback, object>> _items = new Queue<Tuple<SendOrPostCallback, object>>();

            #endregion Private Read-Only Fields

            #region Public Methods

            public void BeginMessageLoop() {
                while (!_done) {
                    Tuple<SendOrPostCallback, object> task = null;
                    lock (_items) {
                        if (_items.Count > 0) {
                            task = _items.Dequeue();
                        }
                    }
                    if (task != null) { task.Item1(task.Item2); }
                    else { _workItemsWaiting.WaitOne(); }
                }
            }

            public void EndMessageLoop() {
                Post(_ => _done = true, null);
            }

            #endregion Public Methods

            #region Public Override Methods

            /// <inheritdoc />
            public override void Send(SendOrPostCallback callback, object state) {
                throw new NotSupportedException("We cannot send to our same thread");
            }

            /// <inheritdoc />
            public override void Post(SendOrPostCallback callback, object state) {
                lock (_items) {
                    _items.Enqueue(Tuple.Create(callback, state));
                }
                _workItemsWaiting.Set();
            }

            /// <inheritdoc />
            public override SynchronizationContext CreateCopy() {
                return this;
            }

            #endregion Public Override Methods

            #endregion Private Inner Classes
        }
    }
}