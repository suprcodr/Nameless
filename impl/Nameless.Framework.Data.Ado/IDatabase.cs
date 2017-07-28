using System;
using System.Collections.Generic;
using System.Data;

namespace Nameless.Framework.Data.Ado {

    public interface IDatabase {

        #region Methods

        int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters);

        TResult ExecuteScalar<TResult>(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters);

        IEnumerable<TResult> ExecuteReader<TResult>(string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters);

        #endregion Methods
    }
}