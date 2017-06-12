using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data {

    public interface IRepository {

        #region Methods

        Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken) where TEntity : class;

        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        Task<dynamic> ExecuteDirectiveAsync<TDirective>(dynamic parameters, CancellationToken cancellationToken) where TDirective : IDirective;

        #endregion Methods
    }
}