using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="IRepository"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public class NullRepository : IRepository {

        #region Public Static Fields

        /// <summary>
        /// Gets the static current instance of <see cref="NullRepository"/>.
        /// </summary>
        public static readonly IRepository Instance = new NullRepository();

        #endregion Public Static Fields

        #region Private Constructors

        // Block construction of NullRepository
        private NullRepository() { }

        #endregion Private Constructors

        #region IRepository Members

        /// <inheritdoc />
        public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<dynamic> ExecuteDirectiveAsync<TDirective>(dynamic parameters, CancellationToken cancellationToken = default(CancellationToken)) where TDirective : IDirective {
            return null;
        }

        /// <inheritdoc />
        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(default(TEntity));
        }

        /// <inheritdoc />
        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(default(TEntity));
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return Enumerable.Empty<TEntity>().AsQueryable();
        }

        /// <inheritdoc />
        public Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.CompletedTask;
        }

        #endregion IRepository Members
    }
}