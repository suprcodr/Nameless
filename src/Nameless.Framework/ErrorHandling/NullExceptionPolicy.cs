using System;

namespace Nameless.Framework.ErrorHandling {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="IExceptionPolicy"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullExceptionPolicy : IExceptionPolicy {

        #region Public Static Fields

        /// <summary>
        /// Gets the static current instance of <see cref="NullExceptionPolicy"/>.
        /// </summary>
        public static readonly IExceptionPolicy Instance = new NullExceptionPolicy();

        #endregion Public Static Fields

        #region Private Constructors

        // Block construction of NullLogger
        private NullExceptionPolicy() { }

        #endregion Private Constructors

        #region IExceptionPolicy Members

        /// <inheritdoc />
        public bool Handle(object sender, Exception exception) {
            return true;
        }

        #endregion IExceptionPolicy Members
    }
}