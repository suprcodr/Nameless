namespace Nameless.Skeleton.Framework.Network.Email {

    /// <summary>
    /// The configuration section class for <see cref="EmailService"/>.
    /// </summary>
    public class EmailSettings {

        #region Public Properties

        /// <summary>
        /// Gets or sets the SMTP server address.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Gets or sets if use SMTP server port.
        /// </summary>
        public bool UsePort { get; set; }

        /// <summary>
        /// Gets or sets the SMTP server port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets if should use credentials.
        /// </summary>
        public bool UseCredentials { get; set; }

        /// <summary>
        /// Gets or sets the user name credential.
        /// </summary>
        public string CredentialUserName { get; set; }

        /// <summary>
        /// Gets or sets the password credential.
        /// </summary>
        public string CredentialPassword { get; set; }

        /// <summary>
        /// Gets or sets if should enable SSL.
        /// </summary>
        public bool EnableSsl { get; set; }

        #endregion Public Properties
    }
}