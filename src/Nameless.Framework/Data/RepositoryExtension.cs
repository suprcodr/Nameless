using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data {

    public static class RepositoryExtension {

        #region Public Static Methods

        public static Task SaveAsync<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            return SaveAsync(source, entity, CancellationToken.None);
        }

        public static Task SaveAsync<TEntity>(this IRepository source, TEntity entity, CancellationToken cancellationToken) where TEntity : class {
            if (source == null) { return Task.CompletedTask; }

            return Task.Run(() => source.Save(entity), cancellationToken);
        }

        public static Task DeleteAsync<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            return DeleteAsync(source, entity, CancellationToken.None);
        }

        public static Task DeleteAsync<TEntity>(this IRepository source, TEntity entity, CancellationToken cancellationToken) where TEntity : class {
            if (source == null) { return Task.CompletedTask; }

            return Task.Run(() => source.Delete(entity), cancellationToken);
        }

        public static Task<TEntity> FindOneAsync<TEntity>(this IRepository source, object id) where TEntity : class {
            return FindOneAsync<TEntity>(source, id, CancellationToken.None);
        }

        public static Task<TEntity> FindOneAsync<TEntity>(this IRepository source, object id, CancellationToken cancellationToken) where TEntity : class {
            if (source == null) { return Task.FromResult(default(TEntity)); }

            return Task.Run(() => source.FindOne<TEntity>(id), cancellationToken);
        }

        public static Task<TEntity> FindOneAsync<TEntity>(this IRepository source, Expression<Func<TEntity, bool>> where) where TEntity : class {
            return FindOneAsync(source, where, CancellationToken.None);
        }

        public static Task<TEntity> FindOneAsync<TEntity>(this IRepository source, Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken) where TEntity : class {
            if (source == null) { return Task.FromResult(default(TEntity)); }

            return Task.Run(() => source.FindOne(where), cancellationToken);
        }

        public static Task<dynamic> ExecuteDirectiveAsync<TDirective>(this IRepository source, dynamic parameters) where TDirective : IDirective {
            return ExecuteDirectiveAsync(source, parameters, CancellationToken.None);
        }

        public static Task<dynamic> ExecuteDirectiveAsync<TDirective>(this IRepository source, dynamic parameters, CancellationToken cancellationToken) where TDirective : IDirective {
            if (source == null) { return Task.FromResult(default(dynamic)); }

            return Task.Run(() => source.ExecuteDirective<TDirective>(parameters), cancellationToken);
        }

        #endregion Public Static Methods
    }
}