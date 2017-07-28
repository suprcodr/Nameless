using System;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains {

    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand {

        #region Protected Properties

        protected IApplicationContext AppContext { get; }
        protected IDatabase Database { get; }

        #endregion Protected Properties

        #region Protected Constructors

        protected CommandHandlerBase(IDatabase database)
            : this(NullApplicationContext.Instance, database) { }

        protected CommandHandlerBase(IApplicationContext appContext, IDatabase database) {
            Prevent.ParameterNull(appContext, nameof(appContext));
            Prevent.ParameterNull(database, nameof(database));

            AppContext = appContext;
            Database = database;
        }

        #endregion Protected Constructors

        #region ICommandHandler<TCommand> Members

        public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null);

        #endregion ICommandHandler<TCommand> Members
    }
}