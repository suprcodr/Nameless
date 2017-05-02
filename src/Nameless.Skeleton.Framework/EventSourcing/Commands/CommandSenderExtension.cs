using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Skeleton.Framework.EventSourcing.Commands {

    /// <summary>
    /// Extension methods for <see cref="ICommandSender"/>.
    /// </summary>
    public static class CommandSenderExtension {

        #region Public Static Methods

        /// <summary>
        /// Sends a command asynchronous.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandSender"/></param>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task SendAsync<TCommand>(this ICommandSender source, TCommand command)
            where TCommand : ICommand {
            return SendAsync(source, command, CancellationToken.None);
        }

        /// <summary>
        /// Sends a command asynchronous.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandSender"/></param>
        /// <param name="command">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task SendAsync<TCommand>(this ICommandSender source, TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand {
            Prevent.ParameterNull(source, nameof(source));

            return Task.Run(() => source.Send(command), cancellationToken);
        }

        #endregion Public Static Methods
    }
}