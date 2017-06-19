using System;
using System.Linq.Expressions;
using System.Threading;

namespace Nameless.Framework.Data {

    /// <summary>
    /// Extension methods for <see cref="IRepository"/>
    /// </summary>
    public static class RepositoryExtension {

        #region Public Static Methods

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="entity">The entity instance.</param>
        public static void Save<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            if (source == null) { return; }

            source.SaveAsync(entity).WaitForResult();
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="entity">The entity instance.</param>
        public static void Delete<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            if (source == null) { return; }

            source.DeleteAsync(entity).WaitForResult();
        }

        /// <summary>
        /// Finds one entity by its ID.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="id">The entity ID.</param>
        /// <returns>The result.</returns>
        public static TEntity FindOne<TEntity>(this IRepository source, object id) where TEntity : class {
            if (source == null) { return default(TEntity); }

            return source.FindOneAsync<TEntity>(id).WaitForResult();
        }

        /// <summary>
        /// Finds one entity by the expression.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="where">The WHERE clause.</param>
        /// <returns>The result.</returns>
        public static TEntity FindOne<TEntity>(this IRepository source, Expression<Func<TEntity, bool>> where) where TEntity : class {
            if (source == null) { return default(TEntity); }

            return source.FindOneAsync(where).WaitForResult();
        }

        /// <summary>
        /// Executes one directive.
        /// </summary>
        /// <typeparam name="TDirective">The directive type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="parameters">The directive parameters.</param>
        /// <returns>The result.</returns>
        public static dynamic ExecuteDirective<TDirective>(this IRepository source, dynamic parameters) where TDirective : IDirective {
            if (source == null) { return default(dynamic); }

            return source.ExecuteDirectiveAsync(parameters, CancellationToken.None).WaitForResult();
        }

        #endregion Public Static Methods
    }
}