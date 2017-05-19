using System;
using Nameless.Skeleton.Framework.Text.Properties;

namespace Nameless.Skeleton.Framework.Text {

    /// <summary>
    /// Implementation of <see cref="TextExpression"/> that uses the <see cref="string.Format(IFormatProvider, string, object)"/> functionality.
    /// </summary>
    public sealed class FormatTextExpression : TextExpression {
        #region	Private Read-Only Fields

        private readonly IDataBinder _dataBinder;
        private readonly string _expression;
        private readonly string _format;

        #endregion

        #region	Public Properties

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public string Expression {
            get { return _expression; }
        }

        /// <summary>
        /// Gets the format.
        /// </summary>
        public string Format {
            get { return _format; }
        }

        #endregion

        #region	Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="FormatTextExpression"/>.
        /// </summary>
        /// <param name="dataBinder">An instance of <see cref="IDataBinder"/>.</param>
        /// <param name="expression">The text expression.</param>
        public FormatTextExpression(IDataBinder dataBinder, string expression) {
            Prevent.ParameterNull(dataBinder, nameof(dataBinder));
            Prevent.ParameterNull(expression, nameof(expression));

            if (!expression.StartsWith("{") || !expression.EndsWith("}")) {
                throw new FormatException(Resources.InvalidExpression);
            }

            _dataBinder = dataBinder;

            var expressionWithoutBraces = expression.Substring(1, expression.Length - 2);
            var colonIndex = expressionWithoutBraces.IndexOf(':');

            if (colonIndex > 0) {
                _expression = expressionWithoutBraces.Substring(0, colonIndex);
                _format = expressionWithoutBraces.Substring(colonIndex + 1);
            } else { _expression = expressionWithoutBraces; }
        }

        #endregion

        #region Public Override Methods

        /// <inheritdoc />
        public override string Eval(object obj) {
            Prevent.ParameterNull(obj, nameof(obj));

            return (!string.IsNullOrWhiteSpace(Format)
                    ? _dataBinder.Eval(obj, Expression, string.Concat("{0:", Format, "}"))
                    : _dataBinder.Eval(obj, Expression)).ToString();
        }

        #endregion
    }
}