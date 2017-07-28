using System;
using Microsoft.AspNetCore.Identity;

namespace Nameless.WebApplication.Core.Identity.Models {

    /// <summary>
    /// Represents an user identity login.
    /// </summary>
    public class UserLogin {

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _userID;
#pragma warning restore 0649

        #endregion Private Read-Only Fields

        #region Public Virtual Properties

        /// <summary>
        /// Gets the associated user ID.
        /// </summary>
        public virtual Guid UserID {
            get { return _userID; }
        }

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

        #region Public Constructors

        public UserLogin() {
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal UserLogin(Guid userID) {
            _userID = userID;
        }

        #endregion Internal Constructors

        #region Public Static Methods

        public static UserLoginInfo ToUserLoginInfo(UserLogin userLogin) {
            if (userLogin == null) { return null; }

            return new UserLoginInfo(userLogin.LoginProvider, userLogin.ProviderKey, userLogin.DisplayName);
        }

        public static UserLogin ToUserLogin(UserLoginInfo userLoginInfo, Guid userID = default(Guid)) {
            if (userLoginInfo == null) { return null; }

            return new UserLogin(userID) {
                DisplayName = userLoginInfo.ProviderDisplayName,
                LoginProvider = userLoginInfo.LoginProvider,
                ProviderKey = userLoginInfo.ProviderKey
            };
        }

        #endregion Public Static Methods

        #region Public Virtual Methods

        public virtual bool Equals(UserLogin obj) {
            return obj != null &&
                   obj.UserID == UserID &&
                   string.Equals(obj.LoginProvider, LoginProvider, StringComparison.CurrentCultureIgnoreCase);
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
                hash += UserID.GetHashCode() * 7;
                hash += (LoginProvider ?? string.Empty).GetHashCode() * 7;
            }
            return hash;
        }

        #endregion Public Override Methods
    }
}