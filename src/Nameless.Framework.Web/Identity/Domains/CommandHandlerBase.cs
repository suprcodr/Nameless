using System;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Generic;

namespace Nameless.Framework.Web.Identity.Domains {

    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand {

        #region Protected Properties

        protected IRepository Repository { get; }

        #endregion Protected Properties

        #region Protected Constructors

        protected CommandHandlerBase(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            Repository = repository;
        }

        #endregion Protected Constructors

        #region ICommandHandler<TCommand> Members

        public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null);

        #endregion ICommandHandler<TCommand> Members
    }
}