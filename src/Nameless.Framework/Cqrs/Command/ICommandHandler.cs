using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Command handler interface.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command.</typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand {

        #region Methods

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="command">The command instance.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous command handler operation.</returns>
        Task HandleAsync(TCommand command, CancellationToken cancellationToken);

        #endregion Methods
    }
}