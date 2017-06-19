using System.Threading;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Extension methods for <see cref="ICommandHandler{TCommand}"/>
    /// </summary>
    public static class CommandHandlerExtension {

        #region Public Static Methods

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandHandler{TCommand}"/></param>
        /// <param name="command">The command.</param>
        public static void Handle<TCommand>(this ICommandHandler<TCommand> source, TCommand command) where TCommand : ICommand {
            if (source == null) { return; }

            source.HandleAsync(command, CancellationToken.None).WaitForResult();
        }

        #endregion Public Static Methods
    }
}