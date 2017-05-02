﻿using System;

namespace Nameless.Skeleton {

    /// <summary>
    /// Represents an exception that cannot be recovered.
    /// </summary>
    public class FatalException : Exception {

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of<see cref="FatalException"/>.
        /// </summary>
        public FatalException() {
        }

        /// <summary>
        /// Initializes a new instance of<see cref="FatalException"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public FatalException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of<see cref="FatalException"/>.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="inner">The inner exception.</param>
        public FatalException(string message, Exception inner) : base(message, inner) {
        }

        #endregion Public Constructors
    }
}