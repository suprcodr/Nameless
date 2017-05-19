using System;

namespace Nameless.Framework.Logging {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="ILogger"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullLogger : ILogger {

        #region Private Static Fields

        private static readonly ILogger CurrentInstance = new NullLogger();

        #endregion Private Static Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the static current instance of <see cref="NullLogger"/>.
        /// </summary>
        public static ILogger Instance {
            get { return CurrentInstance; }
        }

        #endregion Public Static Properties

        #region Private Constructors

        // Block construction of NullLogger
        private NullLogger() { }

        #endregion Private Constructors

        #region ILogger Members

        /// <inheritdoc />
        public bool IsEnabled(LogLevel level) {
            return false;
        }

        /// <inheritdoc />
        public void Log(LogLevel level, Exception exception, string format, params object[] args) { }

        #endregion ILogger Members
    }
}