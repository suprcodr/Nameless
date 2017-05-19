namespace Nameless.Framework.Cqrs.Query {

    /// <summary>
    /// Query handler interface.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult> {

        #region Methods

        /// <summary>
        /// Handles the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The result.</returns>
        TResult Handle(TQuery query);

        #endregion Methods
    }
}