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
        /// <returns>A <see cref="Task{TResult}"/> object for the query execution.</returns>
        public static Task<TResult> SendAsync<TResult>(this IQueryDispatcher source, IQuery<TResult> query) {
            return SendAsync(source, query, CancellationToken.None);
        }

        /// <summary>
        /// Sendes a query for execution asynchronous.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="source">The instance of <see cref="IQueryDispatcher"/></param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task{TResult}"/> object for the query execution.</returns>
        public static Task<TResult> SendAsync<TResult>(this IQueryDispatcher source, IQuery<TResult> query, CancellationToken cancellationToken) {
            if (source == null) { return Task.FromResult(default(TResult)); }

            Prevent.ParameterNull(query, nameof(query));

            return Task.Run(() => source.Query(query), cancellationToken);
        }

        #endregion Public Static Methods
    }
}