using System;

namespace Nameless.Framework.Data.Ado {

    public class DatabaseConnectionMissingException : Exception {

        #region Public Constructors

        public DatabaseConnectionMissingException() {
        }

        public DatabaseConnectionMissingException(string name)
            : base(name) { }

        public DatabaseConnectionMissingException(string name, Exception inner)
            : base(name, inner) { }

        #endregion Public Constructors
    }
}