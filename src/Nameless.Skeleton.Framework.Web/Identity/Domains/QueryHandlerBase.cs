using System;
using System.Linq;
using System.Linq.Expressions;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains {

    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Protected Constructors

        protected QueryHandlerBase(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Protected Constructors

        #region Protected Methods

        protected TEntity FindOne<TEntity>(object id) where TEntity : class {
            return _repository.FindOne<TEntity>(id);
        }

        protected TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            return _repository.FindOne(where);
        }

        protected IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return _repository.Query<TEntity>();
        }

        #endregion Protected Methods

        #region IQueryHandler<TQuery, TResult> Members

        public abstract TResult Handle(TQuery query);

        #endregion IQueryHandler<TQuery, TResult> Members
    }
}