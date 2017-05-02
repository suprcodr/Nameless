using System.Data;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.RoleClaims.Queries {

    internal static class Mappers {

        #region Internal Static Methods

        internal static RoleClaim GetRoleClaims(IDataReader reader) {
            return CommonMappers.ExtractRoleClaim(reader);
        }

        #endregion Internal Static Methods
    }
}