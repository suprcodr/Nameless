using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data {

    /// <summary>
    /// Repository interface.
    /// </summary>
    public interface IRepository {

        #region Methods

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity instance.</param>
        /// <param name="cancellationToken">The cancellation token, if any.</param>
        /// <returns>The <see cref="Task"/> representing the save execution.</returns>
        Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity instance.</param>
        /// <param name="cancellationToken">The cancellation token, if any.</param>
        /// <returns>The <see cref="Task"/> representing the delete execution.</returns>
        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        /// <summary>
        /// Finds one entity by its ID.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="id">The entity ID.</param>
        /// <param name="cancellationToken">The cancellation token, if any.</param>
        /// <returns>The <see cref="Task{TResult}"/> representing the find execution.</returns>
        Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        /// <summary>
        /// Finds one entity by the expression.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="where">The WHERE clause.</param>
        /// <param name="cancellationToken">The cancellation token, if any.</param>
        /// <returns>The <see cref="Task{TResult}"/> representing the find execution.</returns>
        Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        /// <summary>
        /// Retrieves an instance of <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>An instance of <see cref="IQueryable{TEntity}"/></returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        /// <summary>
        /// Executes one directive.
        /// </summary>
        /// <typeparam name="TDirective">The directive type.</typeparam>
        /// <param name="parameters">The directive parameters.</param>
        /// <param name="cancellationToken">The cancellation token, if any.</param>
        /// <returns>The <see cref="Task{TResult}"/> representing the directive execution.</returns>
        Task<dynamic> ExecuteDirectiveAsync<TDirective>(dynamic parameters, CancellationToken cancellationToken = default(CancellationToken)) where TDirective : IDirective;

        #endregion Methods
    }
}