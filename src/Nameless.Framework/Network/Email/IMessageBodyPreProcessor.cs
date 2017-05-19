namespace Nameless.Framework.Network.Email {

    /// <summary>
    /// Defines the methods to pre-process the message body.
    /// </summary>
    public interface IMessageBodyPreProcessor {

        #region Methods

        /// <summary>
        /// Pre-process the message body.
        /// </summary>
        /// <param name="body">The text of the message body.</param>
        /// <param name="data">The data for the message body.</param>
        /// <returns>The processed message body.</returns>
        string Process(string body, object data);

        #endregion Methods
    }
}