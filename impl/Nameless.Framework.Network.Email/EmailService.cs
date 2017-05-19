using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Nameless.Framework.Logging;

namespace Nameless.Framework.Network.Email {

    /// <summary>
    /// Default implementation of <see cref="IEmailService"/>.
    /// </summary>
    public class EmailService : IEmailService {

        #region Private Read-Only Fields

        private readonly EmailSettings _emailSettings;

        #endregion Private Read-Only Fields

        #region Public Properties

        private ILogger _logger;

        /// <summary>
        /// Gets or sets the logger system.
        /// </summary>
        public ILogger Logger {
            get { return _logger ?? (_logger = NullLogger.Instance); }
            set { _logger = value ?? NullLogger.Instance; }
        }

        private IMessageBodyPreProcessor _messageBodyPreProcessor;

        /// <summary>
        /// Gets or sets the <see cref="IMessageBodyPreProcessor"/> implementation.
        /// </summary>
        public IMessageBodyPreProcessor MessageBodyPreProcessor {
            get { return _messageBodyPreProcessor ?? (_messageBodyPreProcessor = NullMessageBodyPreProcessor.Instance); }
            set { _messageBodyPreProcessor = value ?? NullMessageBodyPreProcessor.Instance; }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="EmailService"/>.
        /// </summary>
        /// <param name="emailSettings">The e-mail settings.</param>
        public EmailService(EmailSettings emailSettings) {
            Prevent.ParameterNull(emailSettings, nameof(emailSettings));

            _emailSettings = emailSettings;
        }

        #endregion Public Constructors

        #region IEmailService

        /// <inheritdoc />
        public void Send(Message message) {
            SmtpClient client = null;

            try {
                client = new SmtpClient();

                if (_emailSettings.UsePort) {
                    client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.SslOnConnect);
                } else {
                    client.Connect(_emailSettings.SmtpServer, options: SecureSocketOptions.SslOnConnect);
                }

                if (_emailSettings.UseCredentials) {
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailSettings.CredentialUserName, _emailSettings.CredentialPassword);
                }

                var processedBody = MessageBodyPreProcessor.Process(message.Body, message.BodyData);
                var mail = new MimeMessage {
                    Body = new TextPart(TextFormat.Plain) {
                        Text = processedBody
                    },
                    Sender = MailboxAddress.Parse(message.Sender),
                    Subject = message.Subject
                };

                switch (message.Priority) {
                    case MessagePriority.Medium:
                        mail.Priority = MimeKit.MessagePriority.Normal;
                        break;

                    case MessagePriority.Low:
                        mail.Priority = MimeKit.MessagePriority.NonUrgent;
                        break;

                    case MessagePriority.High:
                        mail.Priority = MimeKit.MessagePriority.Urgent;
                        break;
                }

                mail.From.Add(InternetAddress.Parse(message.From));
                message.Bcc.Each(_ => mail.Bcc.Add(InternetAddress.Parse(_)));
                message.Cc.Each(_ => mail.Cc.Add(InternetAddress.Parse(_)));
                message.To.Each(_ => mail.To.Add(InternetAddress.Parse(_)));

                client.Send(mail);
            } catch (Exception ex) { Logger.Error(ex.Message, ex); throw; } finally { if (client != null) { client.Dispose(); } }
        }

        #endregion IEmailService
    }
}