using System;
using System.Security.Claims;

namespace Nameless.Framework.Web.Identity.Models {

    /// <summary>
    /// Represents n role identity claim.
    /// </summary>
    public class RoleClaim {

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _id;
#pragma warning restore 0649

        #endregion Private Read-Only Fields

        #region Public Virtual Properties

        /// <summary>
        /// Gets the role claim ID.
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
        /// Gets or sets the role associated to the claim.
        /// </summary>
        public virtual Role Role { get; set; }

        #endregion Public Virtual Properties

        #region Public Constructors

        public RoleClaim() {
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal RoleClaim(Guid id) {
            _id = id;
        }

        #endregion Internal Constructors

        #region Public Static Methods

        public static RoleClaim ToRoleClaim(Claim claim) {
            if (claim == null) { return null; }

            return new RoleClaim {
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
                   obj.Type == Type &&
                   obj.Value == Value &&
                   (obj.Role != null ? obj.Role.ID : Guid.Empty) == (Role != null ? Role.ID : Guid.Empty);
        }

        #endregion Public Virtual Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as RoleClaim);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return ID.GetHashCode();
        }

        #endregion Public Override Methods
    }
}