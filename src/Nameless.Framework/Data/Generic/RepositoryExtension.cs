using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace Nameless.Framework.Data.Generic {

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
        /// <param name="entities">The entities to persist.</param>
        public static void Save<TEntity>(this IRepository source, params TEntity[] entities) where TEntity : class {
            if (source == null) { return; }

            source.SaveAsync(CancellationToken.None, NullProgress<int>.Instance, entities).Wait();
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="entities">The entities to remove.</param>
        public static void Delete<TEntity>(this IRepository source, params TEntity[] entities) where TEntity : class {
            if (source == null) { return; }

            source.DeleteAsync(CancellationToken.None, NullProgress<int>.Instance, entities).Wait();
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

            return source.FindOneAsync<TEntity>(id).WaitWithResult();
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

            return source.FindOneAsync(where).WaitWithResult();
        }

        /// <summary>
        /// Finds one entity by the expression.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="where">The WHERE clause.</param>
        /// <returns>The result.</returns>
        public static IEnumerable<TEntity> FindAll<TEntity>(this IRepository source, Expression<Func<TEntity, bool>> where) where TEntity : class {
            if (source == null) { return Enumerable.Empty<TEntity>(); }

            return source.FindAllAsync(where).WaitWithResult();
        }

        /// <summary>
        /// Executes one directive.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <typeparam name="TDirective">The directive type.</typeparam>
        /// <param name="source">The source, in this case, the implementation of <see cref="IRepository"/>.</param>
        /// <param name="parameters">The directive parameters.</param>
        /// <returns>The result.</returns>
        public static TResult ExecuteDirective<TResult, TDirective>(this IRepository source, dynamic parameters) where TDirective : IDirective<TResult> {
            if (source == null) { return default(dynamic); }

            return source.ExecuteDirectiveAsync(parameters, CancellationToken.None).WaitForResult();
        }

        #endregion Public Static Methods
    }
}