using System.Data;

namespace Nameless.Framework.Data.Ado {

    public interface IDbConnectionProvider {

        #region Methods

        IDbConnection CreateConnection(string providerName, string connectionString);

        #endregion Methods
    }
}