using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Data;

namespace Nameless.Framework.Web.Identity.Domains {

    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Protected Constructors

        protected CommandHandlerBase(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Protected Constructors

        #region Protected Methods

        protected void Save<TEntity>(TEntity entity) where TEntity : class {
            _repository.Save(entity);
        }

        protected void Delete<TEntity>(TEntity entity) where TEntity : class {
            _repository.Delete(entity);
        }

        #endregion Protected Methods

        #region ICommandHandler<TCommand> Members

        public abstract void Handle(TCommand command);

        #endregion ICommandHandler<TCommand> Members
    }
}