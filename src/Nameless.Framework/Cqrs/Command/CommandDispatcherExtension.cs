using System;
using System.Threading;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Extension methods for <see cref="ICommandDispatcher"/>.
    /// </summary>
    public static class CommandDispatcherExtension {

        #region Public Static Methods

        /// <summary>
        /// Sends a command.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="source">The source, in this case, an implementation of <see cref="ICommandDispatcher"/></param>
        /// <param name="command">The command.</param>
        /// <param name="progress">The progress notifier.</param>
        public static void Command<TCommand>(this ICommandDispatcher source, TCommand command, IProgress<int> progress = null) where TCommand : ICommand {
            source.CommandAsync(command, progress, CancellationToken.None).WaitForResult();
        }

        #endregion Public Static Methods
    }
}