namespace Nameless.Framework.Data.Sql.Ado {

    public class DbProviderSettings {

        #region Public Static Read-Only Fields

        public static readonly DbProviderSettings Default = new DbProviderSettings {
            DbProviderFactoryName = "SqlClientFactory"
        };

        #endregion

        #region Public Properties

        public string DbProviderFactoryName { get; set; }

        #endregion Public Properties
    }
}