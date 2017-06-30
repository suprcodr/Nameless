using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Nameless.Framework.Data.NoSql.Mongo {

    public sealed class Repository : IRepository {

        #region Private Read-Only Fields

        private readonly IMongoDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public Repository(IMongoDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region Private Methods

        private IMongoCollection<TEntity> GetCollection<TEntity>() {
            return _database.GetCollection<TEntity>(typeof(TEntity).FullName);
        }

        #endregion Private Methods

        #region IRepository Members

        public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            var collection = GetCollection<TEntity>();
            var name = IDAttribute.GetName<TEntity>();
            var id = IDAttribute.GetValue(entity);
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            return GetCollection<TEntity>().DeleteOneAsync(queryID, cancellationToken);
        }

        public Task<dynamic> ExecuteDirectiveAsync<TDirective>(dynamic parameters, CancellationToken cancellationToken = default(CancellationToken)) where TDirective : IDirective {
            if (!typeof(Directive).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive)}\"");
            }

            var directive = (IDirective)Activator.CreateInstance(typeof(TDirective), new object[] { _database });

            return directive.ExecuteAsync(parameters, cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            var name = IDAttribute.GetName<TEntity>();
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            return GetCollection<TEntity>().FindSync(queryID).SingleAsync(cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return GetCollection<TEntity>().FindSync(where).SingleAsync(cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return GetCollection<TEntity>().AsQueryable();
        }

        public Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            var collection = GetCollection<TEntity>();
            var name = IDAttribute.GetName<TEntity>();
            var id = IDAttribute.GetValue(entity);
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            return GetCollection<TEntity>().ReplaceOneAsync(queryID, entity, new UpdateOptions { IsUpsert = true });
        }

        #endregion IRepository Members
    }
}