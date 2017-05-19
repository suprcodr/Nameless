using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.EventSourcing.Commands {

    /// <summary>
    /// Extension methods for <see cref="ICommandHandler{TCommand}"/>
    /// </summary>
    public static class CommandHandlerExtension {

        #region Public Static Methods

        /// <summary>
        /// Executes a command asynchronous.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandHandler{TCommand}"/></param>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task HandleAsync<TCommand>(this ICommandHandler<TCommand> source, TCommand command)
            where TCommand : ICommand {
            return HandleAsync(source, command, CancellationToken.None);
        }

        /// <summary>
        /// Executes a command asynchronous.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandHandler{TCommand}"/></param>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task HandleAsync<TCommand>(this ICommandHandler<TCommand> source, TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand {
            Prevent.ParameterNull(source, nameof(source));

            return Task.Run(() => source.Handle(command), cancellationToken);
        }

        #endregion Public Static Methods
    }
}