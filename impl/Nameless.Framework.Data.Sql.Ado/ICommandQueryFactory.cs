namespace Nameless.Framework.Data.Sql.Ado {

    public interface ICommandQueryFactory {

        #region Methods

        ICommandQuery<TEntity> Create<TEntity>() where TEntity : class;

        #endregion Methods
    }
}