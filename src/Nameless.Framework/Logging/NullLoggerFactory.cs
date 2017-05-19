using System;

namespace Nameless.Framework.Logging {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullLoggerFactory : ILoggerFactory {

        #region Private Static Read-Only Fields

        private static readonly ILoggerFactory CurrentInstance = new NullLoggerFactory();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the static current instance of <see cref="NullLoggerFactory"/>.
        /// </summary>
        public static ILoggerFactory Instance {
            get { return CurrentInstance; }
        }

        #endregion Public Static Properties

        #region Private Constructors

        // Block construction of NullLoggerFactory
        private NullLoggerFactory() { }

        #endregion Private Constructors

        #region ILoggerFactory Members

        /// <inheritdoc />
        public ILogger CreateLogger(Type type) {
            return NullLogger.Instance;
        }

        #endregion ILoggerFactory Members
    }
}