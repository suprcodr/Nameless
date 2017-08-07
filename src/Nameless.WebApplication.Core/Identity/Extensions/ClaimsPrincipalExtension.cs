using System.Security.Claims;

namespace Nameless.WebApplication.Core.Identity {

    public static class ClaimsPrincipalExtension {

        #region Public Static Read-Only Fields

        public static readonly string IsImpersonatingKey = "IsImpersonating";
        public static readonly string IsImpersonatingValue = "true";

        #endregion

        #region Public Static Methods

        // https://stackoverflow.com/a/35577673/809357
        public static string GetUserID(this ClaimsPrincipal source) {
            if (source == null) { return null; }

            return source.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static bool IsImpersonating(this ClaimsPrincipal source) {
            if (source == null) { return false; }

            return source.HasClaim(IsImpersonatingKey, IsImpersonatingValue);
        }

        #endregion Public Static Methods
    }
}