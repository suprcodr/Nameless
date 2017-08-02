using Nameless.WebApplication.Core.Models;

namespace Nameless.WebApplication.Core {

    public interface IApplicationContext {

        #region Properties

        Owner Owner { get; }

        bool IsRootUser { get; }

        #endregion Properties
    }

    /// <summary>
    /// Null Object Pattern implementation for IApplicationContext. (see: https://en.wikipedia.org/wiki/Null_Object_pattern)
    /// </summary>
    public sealed class NullApplicationContext : IApplicationContext {

        #region Private Static Read-Only Fields

        private static readonly IApplicationContext _instance = new NullApplicationContext();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of NullApplicationContext.
        /// </summary>
        public static IApplicationContext Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static NullApplicationContext() {
        }

        #endregion Static Constructors

        #region Private Constructors

        // Prevents the class from being constructed.
        private NullApplicationContext() {
        }

        #endregion Private Constructors

        #region IApplicationContext Members

        public Owner Owner => Owner.Empty;
        public bool IsRootUser => false;

        #endregion IApplicationContext Members
    }
}