using Microsoft.AspNetCore.Identity;

namespace Nameless.Framework.Web.Identity.Models {

    internal static class UserLoginInfoExtension {

        #region Internal Static Methods

        internal static UserLoginInfo ConvertFromUserLogin(this UserLogin source) {
            if (source == null) { return null; }

            return new UserLoginInfo(source.LoginProvider, source.ProviderKey, source.DisplayName);
        }

        #endregion Internal Static Methods
    }
}