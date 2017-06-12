using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data {

    public static class RepositoryExtension {

        #region Public Static Methods

        public static Task SaveAsync<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            if (source == null) { return Task.CompletedTask; }

            return source.SaveAsync(entity, CancellationToken.None);
        }

        public static async void Save<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            if (source == null) { return; }

            await source.SaveAsync(entity);
        }

        public static Task DeleteAsync<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            if (source == null) { return Task.CompletedTask; }

            return source.DeleteAsync(entity, CancellationToken.None);
        }

        public static async void Delete<TEntity>(this IRepository source, TEntity entity) where TEntity : class {
            if (source == null) { return; }

            await source.DeleteAsync(entity);
        }

        public static Task<TEntity> FindOneAsync<TEntity>(this IRepository source, object id) where TEntity : class {
            if (source == null) { return Task.FromResult(default(TEntity)); }

            return source.FindOneAsync<TEntity>(id, CancellationToken.None);
        }

        public static async Task<TEntity> FindOne<TEntity>(this IRepository source, object id) where TEntity : class {
            if (source == null) { return default(TEntity); }

            return await source.FindOneAsync<TEntity>(id);
        }

        public static Task<TEntity> FindOneAsync<TEntity>(this IRepository source, Expression<Func<TEntity, bool>> where) where TEntity : class {
            if (source == null) { return Task.FromResult(default(TEntity)); }

            return source.FindOneAsync(where, CancellationToken.None);
        }

        public static async Task<TEntity> FindOne<TEntity>(this IRepository source, Expression<Func<TEntity, bool>> where) where TEntity : class {
            if (source == null) { return default(TEntity); }

            return await source.FindOneAsync(where);
        }

        public static Task<dynamic> ExecuteDirectiveAsync<TDirective>(this IRepository source, dynamic parameters) where TDirective : IDirective {
            if (source == null) { return Task.FromResult(default(dynamic)); }

            return source.ExecuteDirectiveAsync(parameters, CancellationToken.None);
        }

        public static async Task<dynamic> ExecuteDirective<TDirective>(this IRepository source, dynamic parameters) where TDirective : IDirective {
            if (source == null) { return default(dynamic); }
            
            return await source.ExecuteDirectiveAsync(parameters, CancellationToken.None);
        }

        #endregion Public Static Methods
    }
}