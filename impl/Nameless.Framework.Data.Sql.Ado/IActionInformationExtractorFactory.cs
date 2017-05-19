namespace Nameless.Framework.Data.Sql.Ado {

    public interface IActionInformationExtractorFactory {

        #region Methods

        IActionInformationExtractor<TEntity> Create<TEntity>() where TEntity : class;


        #endregion Methods
    }
}