using System;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Nameless.Framework.Data.Sql.Ado {

    /// <summary>
    /// Default implementation of <see cref="IDbProvider"/>
    /// </summary>
    public class DbProvider : IDbProvider {

        #region Private Read-Only Fields

        private readonly DbProviderSettings _settings;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public DbProvider(DbProviderSettings settings = null) {
            _settings = settings ?? DbProviderSettings.Default;
        }

        #endregion Public Constructors

        #region IDbProvider Members

        /// <inheritdoc />
        public DbProviderFactory GetFactory() {
            switch (_settings.DbProviderFactoryName) {
                case nameof(SqlClientFactory):
                    return SqlClientFactory.Instance;

                case nameof(SqliteFactory):
                    return SqliteFactory.Instance;

                default:
                    throw new InvalidOperationException($"DB Provider Factory not implemented. Name: {_settings.DbProviderFactoryName}");
            }
        }

        #endregion IDbProvider Members
    }
}