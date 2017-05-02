using System.Data;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Queries {

    internal static class Mappers {

        #region Internal Static Methods

        internal static UserClaim GetUserClaims(IDataReader reader) {
            return CommonMappers.ExtractUserClaim(reader);
        }

        #endregion Internal Static Methods
    }
}