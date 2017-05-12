using System;
using Nameless.Skeleton.Framework.Properties;

namespace Nameless.Skeleton.Framework.Text {

    /// <summary>
    /// Exception for indexer accessor not found.
    /// </summary>
    public class IndexerAccessorNotFoundException : Exception {

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="IndexerAccessorNotFoundException"/>.
        /// </summary>
        public IndexerAccessorNotFoundException()
            : base(Resources.IndexerAccessorNotFound) { }

        /// <summary>
        /// Initializes a new instance of <see cref="IndexerAccessorNotFoundException"/>.
        /// </summary>
        /// <param name="typeName">The type name.</param>
		public IndexerAccessorNotFoundException(string typeName)
            : base(string.Format(Resources.IndexerAccessorNotFoundMF, typeName)) { }

        /// <summary>
        /// Initializes a new instance of <see cref="IndexerAccessorNotFoundException"/>.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="inner">The inner exception, if exists.</param>
		public IndexerAccessorNotFoundException(string typeName, Exception inner)
            : base(string.Format(Resources.IndexerAccessorNotFoundMF, typeName), inner) { }

        #endregion Public Constructors
    }
}