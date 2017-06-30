namespace Nameless.Framework.Data.Sql.Ado {

    public class DatabaseSettings {

        #region Public Static Read-Only Fields

        public static readonly DatabaseSettings Default = new DatabaseSettings {
            ConnectionString = string.Empty
        };

        #endregion Public Static Read-Only Fields

        #region Public Properties

        public string ConnectionString { get; set; } = string.Empty;

        #endregion Public Properties
    }
}