using System;
using System.Collections;
using Nameless.Dynamic;

namespace Nameless.WebApplication.Core.Models {

    public class Owner {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets an empty owner.
        /// </summary>
        public static readonly Owner Empty = new Owner(Guid.Empty) {
            Name = "Anonymous"
        };

        #endregion Public Static Read-Only Fields

        #region Private Read-Only Fields

#pragma warning disable 0649
        private readonly Guid _id;
#pragma warning restore 0649
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
        /// Gets or sets the owner name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the identity role attributes
        /// </summary>
        public virtual dynamic Attributes {
            get { return new DynamicHashtable(_attributes); }
        }

        #endregion Public Virtual Properties

        #region Public Constructors

        public Owner() {
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal Owner(Guid id) {
            _id = id;
        }

        #endregion Internal Constructors

        #region Public Methods

        public bool Equals(Owner obj) {
            return obj != null &&
                   string.Equals(obj.Name, Name, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion Public Methods

        #region Public Override Methods

        public override bool Equals(object obj) {
            return Equals(obj as Owner);
        }

        public override int GetHashCode() {
            var hash = 13;
            unchecked {
                hash += (Name ?? string.Empty).GetHashCode() * 7;
            }
            return hash;
        }

        #endregion Public Override Methods
    }
}