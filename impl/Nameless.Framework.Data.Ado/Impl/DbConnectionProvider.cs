using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Nameless.Framework.Data.Ado {

    public sealed class DbConnectionProvider : IDbConnectionProvider {

        #region Public Constants

        public const string SQL_SERVER_PROVIDER_NAME = "sqlserver";
        public const string SQLITE_PROVIDER_NAME = "sqlite";

        #endregion Public Constants

        #region IDbProvider Members

        public IDbConnection CreateConnection(string providerName, string connectionString) {
            IDbConnection connection;

            switch (providerName) {
                case SQL_SERVER_PROVIDER_NAME:
                    connection = SqlClientFactory.Instance.CreateConnection();
                    break;

                case SQLITE_PROVIDER_NAME:
                    connection = SqliteFactory.Instance.CreateConnection();
                    break;

                default:
                    throw new NotImplementedException("Provider not implemented.");
            }

            connection.ConnectionString = connectionString;
            connection.Open();

            return connection;
        }

        #endregion IDbProvider Members
    }
}