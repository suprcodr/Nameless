using System;

namespace Nameless.Framework.Text {

    /// <summary>
    /// The string padder format provider.
    /// </summary>
	public sealed class StringPadderFormatProvider : IFormatProvider {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Instance of left string padder format provider.
        /// </summary>
        public static readonly StringPadderFormatProvider Left = new StringPadderFormatProvider(StringPadderDirection.Left);

        /// <summary>
        /// Instance of right string padder format provider.
        /// </summary>
		public static readonly StringPadderFormatProvider Right = new StringPadderFormatProvider(StringPadderDirection.Right);

        #endregion Public Static Read-Only Fields

        #region Private Read-Only Fields

        private readonly StringPadderDirection _direction;

        #endregion Private Read-Only Fields

        #region Public Properties

        /// <summary>
        /// Gets the string padder direction.
        /// </summary>
        public StringPadderDirection Direction {
            get { return _direction; }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="StringPadderFormatProvider"/>.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public StringPadderFormatProvider(StringPadderDirection direction = StringPadderDirection.Right) {
            _direction = direction;
        }

        #endregion Public Constructors

        #region IFormatProvider Members

        /// <inheritdoc />
        public object GetFormat(Type formatType) {
            if (formatType == typeof(ICustomFormatter)) {
                return new StringPadderFormatter(Direction);
            }

            return null;
        }

        #endregion IFormatProvider Members
    }
}