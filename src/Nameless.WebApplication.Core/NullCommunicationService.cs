using System.Threading.Tasks;

namespace Nameless.WebApplication.Core {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="ICommunicationService"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public class NullCommunicationService : ICommunicationService {

        #region Private Static Read-Only Fields

        private static readonly ICommunicationService _instance = new NullCommunicationService();

        #endregion Private Static Read-Only Fields

        #region Public Static Read-Only Properties

        /// <summary>
        /// Retrieves a null instance
        /// </summary>
        public static ICommunicationService Instance {
            get { return _instance; }
        }

        #endregion Public Static Read-Only Properties

        #region Private Constructors

        // Prevent class from being constructed.
        private NullCommunicationService() {
        }

        #endregion Private Constructors

        #region IEmailSender Members

        /// <inheritdoc />
        public Task SendEmailAsync(string email, string subject, string message) {
            return Task.FromResult(0);
        }

        #endregion IEmailSender Members

        #region ISmsSender Members

        /// <inheritdoc />
        public Task SendSmsAsync(string number, string message) {
            return Task.FromResult(0);
        }

        #endregion ISmsSender Members
    }
}