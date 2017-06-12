using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Command dispatcher interface.
    /// </summary>
    public interface ICommandDispatcher {

        #region Methods

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="command">The instance of the command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous command execution.</returns>
        Task CommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;

        #endregion Methods
    }
}