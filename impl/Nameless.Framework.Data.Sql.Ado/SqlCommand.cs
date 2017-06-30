using System.Data;

namespace Nameless.Framework.Data.Sql.Ado {

    public abstract class SqlCommand {

        #region Protected Abstract Properties

        protected abstract string Sql { get; }
        protected abstract CommandType CommandType { get; }
        protected abstract Parameter[] Parameters { get; }

        #endregion Protected Abstract Properties

        #region Internal Methods

        internal string GetSql() {
            return Sql;
        }

        internal CommandType GetCommandType() {
            return CommandType;
        }

        internal Parameter[] GetParameters() {
            return Parameters ?? new Parameter[0];
        }

        #endregion Internal Methods
    }
}