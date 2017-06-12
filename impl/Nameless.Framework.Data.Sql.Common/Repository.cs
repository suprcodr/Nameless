using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Sql {

    public class Repository : IRepository, IDisposable {

        #region Private Fields

        private IRepository _decorator;
        private IDbTransaction _transaction;
        private bool _disposed;

        #endregion Private Fields

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Repository"/>
        /// </summary>
        /// <param name="decorator">The instance of <see cref="IRepository"/> to decorate.</param>
        public Repository(IRepository decorator) {
            Prevent.ParameterNull(decorator, nameof(decorator));

            var dbRepository = decorator as IDbConnectionAccessor;
            if (dbRepository == null) {
                throw new InvalidOperationException($"Repository must implements {typeof(IDbConnectionAccessor)}");
            }

            _decorator = decorator;
            _transaction = dbRepository.Connection.BeginTransaction();
        }

        #endregion Internal Constructors

        #region Destructor

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Repository() {
            Dispose(false);
        }

        #endregion Destructor

        #region Private Methods

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_transaction != null) {
                    try { _transaction.Commit(); }
                    catch { _transaction.Rollback(); }
                    finally { _transaction.Dispose(); }
                }
            }

            _transaction = null;
            _decorator = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IRepository Members

        public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class {
            return _decorator.DeleteAsync(entity, cancellationToken);
        }

        public Task<dynamic> ExecuteDirectiveAsync<TDirective>(dynamic parameters, CancellationToken cancellationToken) where TDirective : IDirective {
            return _decorator.ExecuteDirectiveAsync(parameters, cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken) where TEntity : class {
            return _decorator.FindOneAsync<TEntity>(id, cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken) where TEntity : class {
            return _decorator.FindOneAsync(where, cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return _decorator.Query<TEntity>();
        }

        public Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class {
            return _decorator.SaveAsync(entity, cancellationToken);
        }

        #endregion IRepository Members

        #region IDisposable Members

        /// <inheritdoc />
        void IDisposable.Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}