using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    /// <summary>
    /// Null Object Pattern implementation for IRepository. (see: https://en.wikipedia.org/wiki/Null_Object_pattern)
    /// </summary>
    public sealed class NullRepository : IRepository {

        #region Private Static Read-Only Fields

        private static readonly IRepository _instance = new NullRepository();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of NullRepository.
        /// </summary>
        public static IRepository Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static NullRepository() {
        }

        #endregion Static Constructors

        #region Private Constructors

        // Prevents the class from being constructed.
        private NullRepository() {
        }

        #endregion Private Constructors

        #region IRepository Members

        public Task SaveAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return Task.CompletedTask;
        }

        public Task DeleteAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return Task.CompletedTask;
        }

        public Task UpdateAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            return Task.CompletedTask;
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(default(TEntity));
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(default(TEntity));
        }

        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(Enumerable.Empty<TEntity>());
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return Enumerable.Empty<TEntity>().AsQueryable();
        }

        public Task<TResult> ExecuteDirectiveAsync<TResult, TDirective>(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) where TDirective : IDirective<TResult> {
            return Task.FromResult(default(TResult));
        }

        #endregion IRepository Members
    }
}