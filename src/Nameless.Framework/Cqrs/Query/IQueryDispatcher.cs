using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Cqrs.Query {

    /// <summary>
    /// Query dispatcher interface.
    /// </summary>
    public interface IQueryDispatcher {

        #region Methods

        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task{TResult}"/> representing the query execution.</returns>
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);

        #endregion Methods
    }
}