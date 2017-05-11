using System.Data;

namespace Nameless.Skeleton.Framework.Data.Sql {
    public interface IDbRepository {
        #region Properties

        IDbConnection Connection { get; }

        #endregion Properties
    }
}