using System;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Nameless.Skeleton.Framework.Data.Ado {

    /// <summary>
    /// Default implementation of <see cref="IDbProvider"/>
    /// </summary>
    public class DbProvider : IDbProvider {

        #region IDbProvider Members

        /// <inheritdoc />
        public DbProviderFactory GetFactory(Type providerFactoryType) {
            Prevent.ParameterNull(providerFactoryType, nameof(providerFactoryType));

            return GetFactory(providerFactoryType.Name);
        }

        /// <inheritdoc />
        public DbProviderFactory GetFactory(string providerFactoryName) {
            Prevent.ParameterNullOrWhiteSpace(providerFactoryName, nameof(providerFactoryName));

            switch (providerFactoryName) {
                case nameof(SqlClientFactory):
                    return SqlClientFactory.Instance;

                case nameof(SqliteFactory):
                    return SqliteFactory.Instance;

                default:
                    throw new InvalidOperationException("DB Provider Factory not implemented.");
            }
        }

        #endregion IDbProvider Members
    }
}