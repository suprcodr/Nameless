namespace Nameless.Skeleton.Framework.Cqrs.Query {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="IQueryDispatcher"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullQueryDispatcher : IQueryDispatcher {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="NullQueryDispatcher"/>.
        /// </summary>
        public static readonly IQueryDispatcher Instance = new NullQueryDispatcher();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullQueryDispatcher() {
        }

        #endregion Private Constructors

        #region IQueryDispatcher Members

        /// <inheritdoc />
        public TResult Query<TResult>(IQuery<TResult> query) {
            return default(TResult);
        }

        #endregion IQueryDispatcher Members
    }
}