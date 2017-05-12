using System.Data;

namespace Nameless.Skeleton.Framework.Data.Sql.Common {
    public interface IDbConnectionAccessor {
        #region Properties

        IDbConnection Connection { get; }

        #endregion Properties
    }
}