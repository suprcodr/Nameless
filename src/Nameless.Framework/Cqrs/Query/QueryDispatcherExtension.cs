using System.Threading;

namespace Nameless.Framework.CQRS.Query {

    /// <summary>
    /// Extension methods for <see cref="IQueryDispatcher"/>.
    /// </summary>
    public static class QueryDispatcherExtension {

        #region Public Static Methods

        /// <summary>
        /// Sendes a query for execution asynchronous.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="source">The instance of <see cref="IQueryDispatcher"/></param>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public static TResult Query<TResult>(this IQueryDispatcher source, IQuery<TResult> query) {
            return source.QueryAsync(query, CancellationToken.None).WaitWithResult();
        }

        #endregion Public Static Methods
    }
}