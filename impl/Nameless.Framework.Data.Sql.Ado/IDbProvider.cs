using System.Data.Common;

namespace Nameless.Framework.Data.Sql.Ado {

    public interface IDbProvider {

        #region Methods

        DbProviderFactory GetFactory();

        #endregion Methods
    }
}