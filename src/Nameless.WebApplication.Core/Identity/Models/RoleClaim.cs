using System;
using System.Security.Claims;

namespace Nameless.WebApplication.Core.Identity.Models {

    /// <summary>
    /// Represents the role claim.
    /// </summary>
    public class RoleClaim {

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _roleID;
#pragma warning restore 0649

        #endregion Private Read-Only Fields

        #region Public Virtual Properties

        /// <summary>
        /// Gets the associated role ID.
        /// </summary>
        public virtual Guid RoleID {
            get { return _roleID; }
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

        public RoleClaim() {
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal RoleClaim(Guid roleID) {
            _roleID = roleID;
        }

        #endregion Internal Constructors

        #region Public Static Methods

        public static RoleClaim ToRoleClaim(Claim claim, Guid roleID = default(Guid)) {
            if (claim == null) { return null; }

            return new RoleClaim(roleID) {
                Type = claim.Type,
                Value = claim.Value
            };
        }

        public static Claim ToClaim(RoleClaim roleClaim) {
            if (roleClaim == null) { return null; }

            return new Claim(roleClaim.Type, roleClaim.Value);
        }

        #endregion Public Static Methods

        #region Public Virtual Methods

        public virtual bool Equals(RoleClaim obj) {
            return obj != null &&
                   obj.RoleID == RoleID &&
                   string.Equals(obj.Type, Type, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion Public Virtual Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as RoleClaim);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            var hash = 13;
            unchecked {
                hash += RoleID.GetHashCode() * 7;
                hash += (Type ?? string.Empty).GetHashCode() * 7;
            }
            return hash;
        }

        #endregion Public Override Methods
    }
}