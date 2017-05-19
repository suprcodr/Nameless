namespace Nameless.Framework.EventSourcing.Commands {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="ICommandSender"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullCommandSender : ICommandSender {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="NullCommandSender"/>.
        /// </summary>
        public static readonly ICommandSender Instance = new NullCommandSender();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullCommandSender() {
        }

        #endregion Private Constructors

        #region ICommandDispatcher Members

        /// <inheritdoc />
        public void Send<TCommand>(TCommand command) where TCommand : ICommand {
        }

        #endregion ICommandDispatcher Members
    }
}