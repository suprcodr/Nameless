using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Skeleton.Framework.Cqrs.Query {

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
        /// <returns>A <see cref="Task{TResult}"/> object for the query execution.</returns>
        public static Task<TResult> HandleAsync<TQuery, TResult>(this IQueryHandler<TQuery, TResult> source, TQuery query)
            where TQuery : IQuery<TResult> {
            return HandleAsync(source, query, CancellationToken.None);
        }

        /// <summary>
        /// Handles a query asynchronous.
        /// </summary>
        /// <typeparam name="TQuery">Type of the query.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="source">The <see cref="IQueryHandler{TQuery, TResult}"/> implementation instance.</param>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task{TResult}"/> object for the query execution.</returns>
        public static Task<TResult> HandleAsync<TQuery, TResult>(this IQueryHandler<TQuery, TResult> source, TQuery query, CancellationToken cancellationToken)
            where TQuery : IQuery<TResult> {
            if (source == null) { return Task.FromResult(default(TResult)); }

            Prevent.ParameterNull(query, nameof(query));

            return Task.Run(() => source.Handle(query), cancellationToken);
        }

        #endregion Public Static Methods
    }
}