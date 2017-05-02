namespace Nameless.Skeleton.Framework.Cqrs.Query {

    /// <summary>
    /// Query dispatcher interface.
    /// </summary>
    public interface IQueryDispatcher {

        #region Methods

        /// <summary>
        /// Sendes a query to be executed.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns>The result of the query execution.</returns>
        TResult Query<TResult>(IQuery<TResult> query);

        #endregion Methods
    }
}