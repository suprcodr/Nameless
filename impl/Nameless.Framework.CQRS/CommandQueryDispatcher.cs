using System;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.CQRS {

    public sealed class CommandQueryDispatcher : ICommandQueryDispatcher {

        #region Private Read-Only Fields

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public CommandQueryDispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) {
            Prevent.ParameterNull(commandDispatcher, nameof(commandDispatcher));
            Prevent.ParameterNull(queryDispatcher, nameof(queryDispatcher));

            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        #endregion Public Constructors

        #region ICommandQueryDispatcher Members

        public Task CommandAsync<TCommand>(TCommand command, IProgress<int> progress = null, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand {
            return _commandDispatcher.CommandAsync(command, progress, cancellationToken);
        }

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default(CancellationToken)) {
            return _queryDispatcher.QueryAsync(query, cancellationToken);
        }

        #endregion ICommandQueryDispatcher Members
    }
}