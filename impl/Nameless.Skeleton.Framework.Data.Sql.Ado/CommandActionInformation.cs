using System.Data;
using System.Linq;

namespace Nameless.Skeleton.Framework.Data.Sql.Ado {

    public class CommandActionInformation {

        #region Public Properties

        public string Text { get; set; }
        public CommandType Type { get; set; } = CommandType.Text;
        public Parameter[] Parameters { get; set; } = Enumerable.Empty<Parameter>().ToArray();

        #endregion Public Properties
    }
}