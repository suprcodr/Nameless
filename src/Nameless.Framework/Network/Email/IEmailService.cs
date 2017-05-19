namespace Nameless.Framework.Network.Email {

    /// <summary>
    /// Defines the methods for the e-mail service.
    /// </summary>
    public interface IEmailService {

        #region Methods

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(Message message);

        #endregion Methods
    }
}