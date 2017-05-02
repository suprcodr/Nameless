using System;

namespace Nameless.Skeleton {

    /// <summary>
    /// Extension methods for <see cref="EventHandler{TEventArgs}"/>.
    /// </summary>
    public static class EventHandlerExtension {

        #region Public Static Methods

        /// <summary>
        /// Before invokes the handler, checks if it is not <c>null</c>.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event handler.</typeparam>
        /// <param name="evt">The event handler.</param>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> evt, object sender, TEventArgs e) where TEventArgs : EventArgs {
            evt?.Invoke(sender, e);
        }

        #endregion Public Static Methods
    }
}