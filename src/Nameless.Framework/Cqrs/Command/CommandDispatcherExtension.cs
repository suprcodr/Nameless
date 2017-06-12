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
        public static Task CommandAsync<TCommand>(this ICommandDispatcher source, TCommand command) where TCommand : ICommand {
            return source.CommandAsync(command, CancellationToken.None);
        }

        #endregion Public Static Methods
    }
}