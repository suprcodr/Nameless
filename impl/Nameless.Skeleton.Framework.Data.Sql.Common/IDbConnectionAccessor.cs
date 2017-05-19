using System.Data;

namespace Nameless.Skeleton.Framework.Data.Sql {
    public interface IDbConnectionAccessor {
        #region Properties

        IDbConnection Connection { get; }

        #endregion Properties
    }
}