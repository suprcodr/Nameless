using System;
using System.Linq.Expressions;

namespace Nameless.Framework.Data.Sql.Ado {

    public interface IActionInformationExtractor<TEntity> where TEntity : class {

        #region Methods

        CommandActionInformation ForSave(TEntity entity);

        CommandActionInformation ForDelete(TEntity entity);

        QueryActionInformation<TEntity> ForFindOneByID(object id);

        QueryActionInformation<TEntity> ForFindOneByExpression(Expression<Func<TEntity, bool>> expression);

        QueryActionInformation<TEntity> ForQuery();

        #endregion Methods
    }
}