using System;
using Microsoft.AspNetCore.Identity;

namespace Nameless.Framework.Web.Identity.Models {

    /// <summary>
    /// Represents an user identity login.
    /// </summary>
    public class UserLogin {

        #region Public Virtual Properties

        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        public virtual string DisplayName { get; set; }

        #endregion Public Virtual Properties

        #region Public Static Methods

        public static UserLoginInfo ToUserLoginInfo(UserLogin userLogin) {
            if (userLogin == null) { return null; }

            return new UserLoginInfo(userLogin.LoginProvider, userLogin.ProviderKey, userLogin.DisplayName);
        }

        public static UserLogin ToUserLogin(UserLoginInfo userLoginInfo) {
            if (userLoginInfo == null) { return null; }

            return new UserLogin {
                DisplayName = userLoginInfo.ProviderDisplayName,
                LoginProvider = userLoginInfo.LoginProvider,
                ProviderKey = userLoginInfo.ProviderKey
            };
        }

        #endregion Public Static Methods

        #region Public Virtual Methods

        public virtual bool Equals(UserLogin obj) {
            return obj != null &&
                   string.Equals(obj.LoginProvider, LoginProvider, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(obj.ProviderKey, ProviderKey, StringComparison.CurrentCulture) &&
                   string.Equals(obj.DisplayName, DisplayName, StringComparison.CurrentCulture);
        }

        #endregion Public Virtual Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as UserLogin);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            var hash = 13;

            unchecked {
                hash += (LoginProvider != null ? LoginProvider.GetHashCode() : 0) * 7;
                hash += (ProviderKey != null ? ProviderKey.GetHashCode() : 0) * 7;
                hash += (DisplayName != null ? DisplayName.GetHashCode() : 0) * 7;
            }

            return hash;
        }

        #endregion Public Override Methods
    }
}