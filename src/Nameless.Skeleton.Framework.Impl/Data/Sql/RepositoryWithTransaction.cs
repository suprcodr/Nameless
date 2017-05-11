using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Nameless.Skeleton.Framework.Data.Sql {

    internal class RepositoryWithTransaction : IRepository, IDisposable {

        #region Private Fields

        private IRepository _decorator;
        private IDbTransaction _transaction;
        private bool _disposed;

        #endregion Private Fields

        #region Internal Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RepositoryWithTransaction"/>
        /// </summary>
        /// <param name="decorator">The instance of <see cref="IRepository"/> to decorate.</param>
        internal RepositoryWithTransaction(IRepository decorator) {
            Prevent.ParameterNull(decorator, nameof(decorator));

            var dbRepository = decorator as IDbRepository;
            if (dbRepository == null) {
                throw new InvalidOperationException($"Repository must implements {typeof(IDbRepository)}");
            }

            _decorator = decorator;
            _transaction = dbRepository.Connection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        #endregion Internal Constructors

        #region Destructor

        /// <summary>
        /// Destructor.
        /// </summary>
        ~RepositoryWithTransaction() {
            Dispose(false);
        }

        #endregion Destructor

        #region Private Methods

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_transaction != null) {
                    try { _transaction.Commit(); } catch { _transaction.Rollback(); } finally { _transaction.Dispose(); }
                }
            }

            _transaction = null;
            _decorator = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IRepository Members

        public void Delete<TEntity>(TEntity entity) where TEntity : class {
            _decorator.Delete(entity);
        }

        public dynamic ExecuteDirective<TDirective>(dynamic parameters) where TDirective : IDirective {
            return _decorator.ExecuteDirective(parameters);
        }

        public TEntity FindOne<TEntity>(object id) where TEntity : class {
            return _decorator.FindOne<TEntity>(id);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            return _decorator.FindOne(where);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return _decorator.Query<TEntity>();
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class {
            _decorator.Save(entity);
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