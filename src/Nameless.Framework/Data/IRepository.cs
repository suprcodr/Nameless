using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nameless.Framework.Data {

    public interface IRepository {

        #region Methods

        void Save<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        TEntity FindOne<TEntity>(object id) where TEntity : class;

        TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;

        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        dynamic ExecuteDirective<TDirective>(dynamic parameters) where TDirective : IDirective;

        #endregion Methods
    }
}