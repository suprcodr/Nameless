using System.Collections.Generic;

namespace Nameless.Framework.Data.Ado {

    public class DatabaseSettings {

        #region Public Properties

        public List<ConnectionString> ConnectionStrings { get; set; } = new List<ConnectionString>();

        #endregion Public Properties
    }

    public class ConnectionString {

        #region Public Properties

        public string Name { get; set; }

        public string ProviderName { get; set; }

        public string Value { get; set; }

        #endregion Public Properties
    }
}