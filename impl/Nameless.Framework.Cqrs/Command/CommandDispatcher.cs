using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.IoC;

namespace Nameless.Framework.Cqrs.Command {

    public class CommandDispatcher : ICommandDispatcher {

        #region Private Read-Only Fields

        private readonly IResolver _resolver;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public CommandDispatcher(IResolver resolver) {
            Prevent.ParameterNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        #endregion Public Constructors

        #region ICommandDispatcher Members

        public Task CommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand {
            Prevent.ParameterNull(command, nameof(command));

            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _resolver.Resolve(handlerType);

            return handler.HandleAsync((dynamic)command, cancellationToken);
        }

        #endregion ICommandDispatcher Members
    }
}