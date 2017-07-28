using System;
using System.Collections;
using System.Collections.Generic;
using Nameless.Dynamic;
using Nameless.WebApplication.Core.Models;

namespace Nameless.WebApplication.Core.Identity.Models {

    public class Role {

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _id;
#pragma warning restore 0649
        private readonly ICollection<User> _users = new HashSet<User>();
        private readonly ICollection<RoleClaim> _claims = new HashSet<RoleClaim>();
        private readonly IDictionary _attributes = new Hashtable();

        #endregion Private Read-Only Fields

        #region Public Virtual Properties

        /// <summary>
        /// Gets the role ID.
        /// </summary>
        public virtual Guid ID {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the concurrency stamp.
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the role normalized name.
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// Gets the users associated to the current role.
        /// </summary>
        public ICollection<User> Users {
            get { return _users; }
        }

        /// <summary>
        /// Gets the claims associated to the current role.
        /// </summary>
        public ICollection<RoleClaim> Claims {
            get { return _claims; }
        }

        /// <summary>
        /// Gets or sets the identity role attributes
        /// </summary>
        public virtual dynamic Attributes {
            get { return new DynamicHashtable(_attributes); }
        }

        /// <summary>
        /// Gets or sets the entity owner.
        /// </summary>
        public virtual Owner Owner { get; set; }

        #endregion Public Virtual Properties

        #region Public Constructors

        public Role() {
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal Role(Guid id, Owner owner = null) {
            _id = id;
            Owner = owner;
        }

        #endregion Internal Constructors

        #region Public Methods

        public bool Equals(Role obj) {
            return obj != null &&
                   string.Equals(obj.Name, Name, StringComparison.CurrentCultureIgnoreCase) &&
                   (obj.Owner != null ? obj.Owner.ID : Guid.Empty) == (Owner != null ? Owner.ID : Guid.Empty);
        }

        #endregion Public Methods

        #region Public Override Methods

        public override bool Equals(object obj) {
            return Equals(obj as Role);
        }

        public override int GetHashCode() {
            var hash = 13;
            unchecked {
                hash += (Name ?? string.Empty).GetHashCode() * 7;
                hash += (Owner != null ? Owner.ID : Guid.Empty).GetHashCode() * 7;
            }
            return hash;
        }

        #endregion Public Override Methods
    }
}