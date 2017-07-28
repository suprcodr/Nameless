using System.Data;

namespace Nameless.Framework.Data.Ado {

    public interface IDbConnectionAccessor {

        #region Properties

        IDbConnection Connection { get; }

        #endregion Properties
    }
}