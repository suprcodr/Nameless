using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains {

    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult> {

        #region Protected Properties

        protected IApplicationContext AppContext { get; }
        protected IDatabase Database { get; }

        #endregion Protected Properties

        #region Protected Constructors

        protected QueryHandlerBase(IDatabase database)
            : this(NullApplicationContext.Instance, database) { }

        protected QueryHandlerBase(IApplicationContext appContext, IDatabase database) {
            Prevent.ParameterNull(appContext, nameof(appContext));
            Prevent.ParameterNull(database, nameof(database));

            AppContext = appContext;
            Database = database;
        }

        #endregion Protected Constructors

        #region Protected Methods

        /// <summary>
        /// Defines the entities owner parameter.
        /// </summary>
        /// <returns></returns>
        protected Parameter GetOwnerParameter() {
            if (AppContext != null && !AppContext.IsRootUser && AppContext.Owner != null && AppContext.Owner.ID != Guid.Empty) {
                return Parameter.CreateInputParameter(EntitySchema.Owners.Fields.ID, AppContext.Owner.ID, DbType.Guid);
            }

            if (AppContext != null && AppContext.IsRootUser) {
                return Parameter.CreateInputParameter(EntitySchema.Owners.Fields.ID, DBNull.Value, DbType.Guid);
            }

            throw new ApplicationContextUnknownException();
        }

        #endregion Protected Methods

        #region IQueryHandler<TQuery, TResult> Members

        public abstract Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default(CancellationToken));

        #endregion IQueryHandler<TQuery, TResult> Members
    }
}