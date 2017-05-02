using System;
using Nameless.Skeleton.Framework.Properties;

namespace Nameless.Skeleton.Framework.Text {

    /// <summary>
    /// Exception for expression property not found.
    /// </summary>
    public class ExpressionPropertyNotFoundException : Exception {

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionPropertyNotFoundException"/>.
        /// </summary>
        public ExpressionPropertyNotFoundException()
            : base(Resources.ExpressionPropertyNotFound) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionPropertyNotFoundException"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
		public ExpressionPropertyNotFoundException(string expression)
            : base(string.Format(Resources.ExpressionPropertyNotFoundMF, expression)) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionPropertyNotFoundException"/>.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="inner">The inner exception, if exists.</param>
		public ExpressionPropertyNotFoundException(string expression, Exception inner)
            : base(string.Format(Resources.ExpressionPropertyNotFoundMF, expression), inner) { }

        #endregion Public Constructors
    }
}