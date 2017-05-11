using System;
using System.Data;
using System.Linq;

namespace Nameless.Skeleton.Framework.Data.Sql.Ado {

    public class QueryActionInformation<TEntity> where TEntity : class {

        #region Public Properties

        public string Text { get; set; }
        public CommandType Type { get; set; } = CommandType.Text;
        public Parameter[] Parameters { get; set; } = Enumerable.Empty<Parameter>().ToArray();
        public Func<IDataReader, TEntity> Mapper { get; set; }

        #endregion Public Properties
    }
}