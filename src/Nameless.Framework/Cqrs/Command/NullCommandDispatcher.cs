using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="ICommandDispatcher"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullCommandDispatcher : ICommandDispatcher {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="NullCommandDispatcher"/>.
        /// </summary>
        public static readonly ICommandDispatcher Instance = new NullCommandDispatcher();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullCommandDispatcher() {
        }

        #endregion Private Constructors

        #region ICommandDispatcher Members

        /// <inheritdoc />
        public Task CommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand {
            return Task.CompletedTask;
        }

        #endregion ICommandDispatcher Members
    }
}