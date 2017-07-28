using System;
using System.Security.Claims;

namespace Nameless.WebApplication.Core.Identity.Models {

    /// <summary>
    /// Represents an user identity claim.
    /// </summary>
    public class UserClaim {

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
        /// Gets or sets the claim type.
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        public virtual string Value { get; set; }

        #endregion Public Virtual Properties

        #region Public Constructors

        public UserClaim() {
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal UserClaim(Guid userID) {
            _userID = userID;
        }

        #endregion Internal Constructors

        #region Public Static Methods

        public static UserClaim ToUserClaim(Claim claim, Guid userID = default(Guid)) {
            if (claim == null) { return null; }

            return new UserClaim(userID) {
                Type = claim.Type,
                Value = claim.Value
            };
        }

        public static Claim ToClaim(UserClaim userClaim) {
            if (userClaim == null) { return null; }

            return new Claim(userClaim.Type, userClaim.Value);
        }

        #endregion Public Static Methods

        #region Public Virtual Methods

        public virtual bool Equals(UserClaim obj) {
            return obj != null &&
                   obj.UserID == UserID &&
                   string.Equals(obj.Type, Type, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion Public Virtual Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as UserClaim);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            var hash = 13;
            unchecked {
                hash += UserID.GetHashCode() * 7;
                hash += (Type ?? string.Empty).GetHashCode() * 7;
            }
            return hash;
        }

        #endregion Public Override Methods
    }
}