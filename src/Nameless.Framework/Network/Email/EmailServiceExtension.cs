using System.Threading;

namespace Nameless.Framework.Network.Email {

    /// <summary>
    /// Extension methods for <see cref="IEmailService"/>.
    /// </summary>
    public static class EmailServiceExtension {

        #region Public Static Methods

        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="source">The source <see cref="IEmailService"/>.</param>
        /// <param name="message">The message.</param>
        public static void Send(this IEmailService source, Message message) {
            if (source == null) { return; }

            source.SendAsync(message, CancellationToken.None).Wait();
        }

        #endregion Public Static Methods
    }
}