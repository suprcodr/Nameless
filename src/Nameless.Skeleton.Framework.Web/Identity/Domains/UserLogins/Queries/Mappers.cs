using System.Data;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Queries {

    internal static class Mappers {

        #region Internal Static Methods

        internal static User FindUserByLogin(IDataReader reader) {
            return CommonMappers.ExtractUser(reader);
        }

        internal static UserLogin GetUserLogins(IDataReader reader) {
            return CommonMappers.ExtractUserLogin(reader);
        }

        #endregion Internal Static Methods
    }
}