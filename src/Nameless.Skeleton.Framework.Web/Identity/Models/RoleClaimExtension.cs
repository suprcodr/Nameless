using System.Security.Claims;

namespace Nameless.Skeleton.Framework.Web.Identity.Models {

    internal static class RoleClaimExtension {

        #region Internal Static Methods

        internal static RoleClaim ConvertToRoleClaim(this Claim source) {
            if (source == null) { return null; }

            return new RoleClaim {
                Type = source.Type,
                Value = source.Value
            };
        }

        internal static Claim ConvertFromRoleClaim(this RoleClaim source) {
            if (source == null) { return null; }

            return new Claim(source.Type, source.Value);
        }

        #endregion Internal Static Methods
    }
}