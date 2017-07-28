using System.Threading;

namespace Nameless.Framework.CQRS.Query {

    /// <summary>
    /// Extension methods for <see cref="IQueryHandler{TQuery, TResult}"/>.
    /// </summary>
    public static class QueryHandlerExtension {

        #region Public Static Methods

        /// <summary>
        /// Handles a query asynchronous.
        /// </summary>
        /// <typeparam name="TQuery">Type of the query.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="source">The <see cref="IQueryHandler{TQuery, TResult}"/> implementation instance.</param>
        /// <param name="query">The query.</param>
        /// <returns>The query result.</returns>
        public static TResult Handle<TQuery, TResult>(this IQueryHandler<TQuery, TResult> source, TQuery query) where TQuery : IQuery<TResult> {
            return source.HandleAsync(query, CancellationToken.None).WaitWithResult();
        }

        #endregion Public Static Methods
    }
}