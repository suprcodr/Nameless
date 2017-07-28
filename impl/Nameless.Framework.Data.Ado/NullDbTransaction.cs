using System;
using System.Data;

namespace Nameless.Framework.Data.Ado {

    /// <summary>
    /// Null Object Pattern implementation for IDbTransaction. (see: https://en.wikipedia.org/wiki/Null_Object_pattern)
    /// </summary>
    public sealed class NullDbTransaction : IDbTransaction {

        #region Private Static Read-Only Fields

        private static readonly IDbTransaction _instance = new NullDbTransaction();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of NullDbTransaction.
        /// </summary>
        public static IDbTransaction Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static NullDbTransaction() {
        }

        #endregion Static Constructors

        #region Private Constructors

        // Prevents the class from being constructed.
        private NullDbTransaction() {
        }

        #endregion Private Constructors

        #region IDbTransaction Members

        /// <inheritdoc />
        public IDbConnection Connection => null;

        /// <inheritdoc />
        public IsolationLevel IsolationLevel => IsolationLevel.Unspecified;

        /// <inheritdoc />
        public void Commit() { }

        /// <inheritdoc />
        public void Rollback() { }

        /// <inheritdoc />
        public void Dispose() { }

        #endregion IDbTransaction Members
    }
}