using System;

namespace Nameless.WebApplication.Core {

    public class ApplicationContextUnknownException : Exception {

        #region Public Constructors

        public ApplicationContextUnknownException() {
        }

        public ApplicationContextUnknownException(string message)
            : base(message) { }

        public ApplicationContextUnknownException(string message, Exception inner)
            : base(message, inner) { }

        #endregion Public Constructors
    }
}