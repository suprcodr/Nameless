using System;
using System.Linq.Expressions;

namespace Nameless.Framework.Data.Sql.Ado {

    public interface ICommandQuery<TEntity> where TEntity : class {

        #region Methods

        SqlCommand ForSave(TEntity entity);

        SqlCommand ForDelete(TEntity entity);

        SqlQuery<TEntity> ForFindOneByID(object id);

        SqlQuery<TEntity> ForFindOneByExpression(Expression<Func<TEntity, bool>> expression);

        SqlQuery<TEntity> ForQuery();

        #endregion Methods
    }
}