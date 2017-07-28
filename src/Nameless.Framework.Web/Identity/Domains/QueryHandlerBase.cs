using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Generic;

namespace Nameless.Framework.Web.Identity.Domains {

    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> {

        #region Protected Properties

        protected IRepository Repository { get; }

        #endregion Protected Properties

        #region Protected Constructors

        protected QueryHandlerBase(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            Repository = repository;
        }

        #endregion Protected Constructors

        #region IQueryHandler<TQuery, TResult> Members

        public abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default(CancellationToken));

        #endregion IQueryHandler<TQuery, TResult> Members
    }
}