namespace Nameless.Framework.Network.Events {

    /// <summary>
    /// Defines methods for listen to events.
    /// </summary>
	public interface IEventListener {

        #region Methods

        /// <summary>
        /// Triggers a notification from the publisher.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        void OnNotification(IEventPublisher publisher);

        #endregion Methods
    }
}