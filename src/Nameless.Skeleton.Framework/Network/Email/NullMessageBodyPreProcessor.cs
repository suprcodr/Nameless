namespace Nameless.Skeleton.Framework.Network.Email {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="IMessageBodyPreProcessor"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullMessageBodyPreProcessor : IMessageBodyPreProcessor {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="NullMessageBodyPreProcessor"/>.
        /// </summary>
        public static readonly IMessageBodyPreProcessor Instance = new NullMessageBodyPreProcessor();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullMessageBodyPreProcessor() {
        }

        #endregion Private Constructors

        #region IMessageBodyPreProcessor Members

        /// <inheritdoc />
        public string Process(string body, object state) {
            return body;
        }

        #endregion IMessageBodyPreProcessor Members
    }
}