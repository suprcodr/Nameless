using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Query {

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
        /// <returns>A <see cref="Task{TResult}"/> representing the query execution.</returns>
        public static Task<TResult> QueryAsync<TResult>(this IQueryDispatcher source, IQuery<TResult> query) {
            return source.QueryAsync(query, CancellationToken.None);
        }

        #endregion Public Static Methods
    }
}