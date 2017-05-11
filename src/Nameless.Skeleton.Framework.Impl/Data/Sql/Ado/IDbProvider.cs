using System.Data.Common;

namespace Nameless.Skeleton.Framework.Data.Sql.Ado {

    public interface IDbProvider {

        #region Methods

        DbProviderFactory GetFactory();

        #endregion Methods
    }
}