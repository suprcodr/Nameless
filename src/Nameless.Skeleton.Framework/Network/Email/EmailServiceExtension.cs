using System.Threading.Tasks;

namespace Nameless.Skeleton.Framework.Network.Email {

    /// <summary>
    /// Extension methods for <see cref="IEmailService"/>.
    /// </summary>
    public static class EmailServiceExtension {

        #region Public Static Methods

        /// <summary>
        /// Sends message asynchronous.
        /// </summary>
        /// <param name="source">The source <see cref="IEmailService"/>.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static Task SendAsync(this IEmailService source, Message message) {
            return Task.Run(() => source.Send(message));
        }

        #endregion Public Static Methods
    }
}