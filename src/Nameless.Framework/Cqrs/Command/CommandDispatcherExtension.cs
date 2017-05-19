using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Extension methods for <see cref="ICommandDispatcher"/>.
    /// </summary>
    public static class CommandDispatcherExtension {

        #region Public Static Methods

        /// <summary>
        /// Sendes a command asynchronous.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandDispatcher"/></param>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task SendAsync<TCommand>(this ICommandDispatcher source, TCommand command)
            where TCommand : ICommand {
            return SendAsync(source, command, CancellationToken.None);
        }

        /// <summary>
        /// Sendes a command asynchronous.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandDispatcher"/></param>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task SendAsync<TCommand>(this ICommandDispatcher source, TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand {
            Prevent.ParameterNull(source, nameof(source));

            return Task.Run(() => source.Command(command), cancellationToken);
        }

        #endregion Public Static Methods
    }
}