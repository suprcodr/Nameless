using System;
using System.Data;

namespace Nameless.Framework.Data.Sql.Ado {

    public abstract class SqlQuery<TEntity> where TEntity : class {

        #region Protected Abstract Properties

        protected abstract string Sql { get; }
        protected abstract CommandType CommandType { get; }
        protected abstract Parameter[] Parameters { get; }
        protected abstract Func<IDataReader, TEntity> Mapper { get; }

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

        internal Func<IDataReader, TEntity> GetMapper() {
            return Mapper;
        }

        #endregion Internal Methods
    }
}