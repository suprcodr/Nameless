namespace Nameless.Skeleton.Framework.Network.Events {

    /// <summary>
    /// Defines methods to register, unregister and notify listeners.
    /// </summary>
	public interface IEventPublisher {

        #region Methods

        /// <summary>
        /// Registers listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        void RegisterListener(IEventListener listener);

        /// <summary>
        /// Removes listener from notification.
        /// </summary>
        /// <param name="listener">The listener.</param>
		void UnregisterListener(IEventListener listener);

        /// <summary>
        /// Notifies all listeners.
        /// </summary>
		void NotifyListeners();

        #endregion Methods
    }
}