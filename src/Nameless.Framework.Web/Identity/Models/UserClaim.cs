using System;
using System.Security.Claims;

namespace Nameless.Framework.Web.Identity.Models {

    /// <summary>
    /// Represents an user identity claim.
    /// </summary>
    public class UserClaim {

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _id;
#pragma warning restore 0649

        #endregion Private Read-Only Fields

        #region Public Virtual Properties

        /// <summary>
        /// Gets the user claim ID.
        /// </summary>
        public virtual Guid ID {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the claim type.
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Gets or sets the user associated to the claim.
        /// </summary>
        public virtual User User { get; set; }

        #endregion Public Virtual Properties

        #region Public Static Methods

        public static UserClaim ToUserClaim(Claim claim) {
            if (claim == null) { return null; }

            return new UserClaim {
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
                   obj.Type == Type &&
                   obj.Value == Value &&
                   (obj.User != null ? obj.User.ID : Guid.Empty) == (User != null ? User.ID : Guid.Empty);
        }

        #endregion Public Virtual Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as UserClaim);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return ID.GetHashCode();
        }

        #endregion Public Override Methods
    }
}