using System.Security.Claims;

namespace Nameless.Skeleton.Framework.Web.Identity.Models {

    internal static class UserClaimExtension {

        #region Internal Static Methods

        internal static UserClaim ConvertToUserClaim(this Claim source) {
            if (source == null) { return null; }

            return new UserClaim {
                Type = source.Type,
                Value = source.Value
            };
        }

        internal static Claim ConvertFromUserClaim(this UserClaim source) {
            if (source == null) { return null; }

            return new Claim(source.Type, source.Value);
            ;
        }

        #endregion Internal Static Methods
    }
}