using System;
using System.Collections;
using System.Collections.Generic;
using Nameless.Dynamic;
using Nameless.Framework;

namespace Nameless.Framework.Web.Identity.Models {

    /// <summary>
    /// Representing an identity user.
    /// </summary>
    public class User {

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _id;
#pragma warning restore 0649
        private readonly ICollection<Role> _roles = new HashSet<Role>();
        private readonly ICollection<UserClaim> _claims = new HashSet<UserClaim>();
        private readonly ICollection<UserLogin> _logins = new HashSet<UserLogin>();
        private readonly IDictionary _attributes = new Hashtable();

        #endregion Private Read-Only Fields

        #region Public Properties

        /// <summary>
        /// Gets the user Id.
        /// </summary>
        public virtual Guid Id {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the concurrency stamp.
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the normalized version of the user name.
        /// </summary>
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        /// Gets or sets the user full name.
        /// </summary>
        public virtual string FullName { get; set; }

        /// <summary>
        /// Gets or sets the access failed counter.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the e-mail
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets if the e-mail was confirmed.
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the normalized e-mail
        /// </summary>
        public virtual string NormalizedEmail { get; set; }

        /// <summary>
        /// Gets or sets the ability to lockout the identity.
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the lockout end date (UTC).
        /// </summary>
        public virtual DateTimeOffset? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets if the phone number was confirmed.
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets if the two factor authentication is enabled.
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the security stamp.
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        public virtual string ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the profile picture (blob, if any).
        /// </summary>
        public virtual byte[] ProfilePictureBlob { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// Gets the roles associated to the identity.
        /// </summary>
        public virtual ICollection<Role> Roles {
            get { return _roles; }
        }

        /// <summary>
        /// Gets the claims associated to the identity.
        /// </summary>
        public virtual ICollection<UserClaim> Claims {
            get { return _claims; }
        }

        /// <summary>
        /// Gets the logins associated to the identity.
        /// </summary>
        public virtual ICollection<UserLogin> Logins {
            get { return _logins; }
        }

        /// <summary>
        /// Gets the identity user attributes
        /// </summary>
        public virtual dynamic Attributes {
            get { return new DynamicHashtable(_attributes); }
        }

        #endregion Public Properties

        #region Public Constructors

        public User() {
        }

        #endregion Public Constructors

        #region Public Methods

        public bool Equals(User obj) {
            return obj != null && obj.Id == Id;
        }

        #endregion Public Methods

        #region Public Override Methods

        /// <inheritdoc />
        public override bool Equals(object obj) {
            return Equals(obj as User);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        #endregion Public Override Methods
    }
}