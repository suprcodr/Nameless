using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    public interface IQuerier {

        #region Methods

        Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        #endregion Methods
    }
}