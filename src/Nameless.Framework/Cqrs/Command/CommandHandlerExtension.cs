using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Extension methods for <see cref="ICommandHandler{TCommand}"/>
    /// </summary>
    public static class CommandHandlerExtension {

        #region Public Static Methods

        /// <summary>
        /// Executes a command asynchronously.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandHandler{TCommand}"/></param>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the command execution.</returns>
        public static Task HandleAsync<TCommand>(this ICommandHandler<TCommand> source, TCommand command) where TCommand : ICommand {
            if (source == null) { return Task.CompletedTask; }

            return source.HandleAsync(command, CancellationToken.None);
        }

        /// <summary>
        /// Executes a command synchronously.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandHandler{TCommand}"/></param>
        /// <param name="command">The command.</param>
        public static async void Handle<TCommand>(this ICommandHandler<TCommand> source, TCommand command) where TCommand : ICommand {
            if (source == null) { return; }

            await source.HandleAsync(command, CancellationToken.None);
        }

        #endregion Public Static Methods
    }
}