using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the method execution.
        /// </returns>
        Task SendAsync(Message message, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Methods
    }
}