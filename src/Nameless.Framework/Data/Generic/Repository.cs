using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    public sealed class Repository : IRepository {

        #region Private Read-Only Fields

        private readonly IDirectiveExecutor _directiveExecutor;
        private readonly IPersister _persister;
        private readonly IQuerier _querier;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public Repository(IDirectiveExecutor directiveExecutor, IPersister persister, IQuerier querier) {
            Prevent.ParameterNull(directiveExecutor, nameof(directiveExecutor));
            Prevent.ParameterNull(persister, nameof(persister));
            Prevent.ParameterNull(querier, nameof(querier));

            _directiveExecutor = directiveExecutor;
            _persister = persister;
            _querier = querier;
        }

        #endregion Public Constructors

        #region IRepository Members

        public Task SaveAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return _persister.SaveAsync<TEntity>(cancellationToken, progress, entities);
        }

        public Task<TResult> ExecuteDirectiveAsync<TResult, TDirective>(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) where TDirective : IDirective<TResult> {
            return _directiveExecutor.ExecuteDirectiveAsync<TResult, TDirective>(parameters, cancellationToken, progress);
        }

        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return _querier.FindAllAsync<TEntity>(expression, cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return _querier.FindOneAsync<TEntity>(id, cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return _querier.FindOneAsync<TEntity>(expression, cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return _querier.Query<TEntity>();
        }

        public Task DeleteAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return _persister.DeleteAsync<TEntity>(cancellationToken, progress, entities);
        }

        #endregion IRepository Members
    }
}