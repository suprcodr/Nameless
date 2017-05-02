namespace Nameless.Skeleton.Framework.EventSourcing.Messaging {

    /// <summary>
    /// Defines methods/properties/events for a handler to handle a specific message.
    /// </summary>
    /// <typeparam name="TMessage">Type of the message.</typeparam>
    public interface IHandle<in TMessage> where TMessage : IMessage {

        #region Methods

        /// <summary>
        /// Handles a specified message.
        /// </summary>
        /// <param name="message">The message instance.</param>
        void Handle(TMessage message);

        #endregion Methods
    }
}