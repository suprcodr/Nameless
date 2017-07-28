using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Nameless.Framework.Data.Generic.NoSql.Mongo {

    public sealed class Querier : IQuerier {
        
        #region Private Read-Only Fields

        private readonly IMongoDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public Querier(IMongoDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region Private Methods

        private IMongoCollection<TEntity> GetCollection<TEntity>() {
            return _database.GetCollection<TEntity>(typeof(TEntity).FullName);
        }

        #endregion Private Methods

        #region IQuerier Members

        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.Run(() => GetCollection<TEntity>().FindSync(expression, cancellationToken: cancellationToken).Current);
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            var name = IDAttribute.GetName<TEntity>();
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            return GetCollection<TEntity>().FindSync(queryID).SingleAsync(cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return GetCollection<TEntity>().FindSync(expression).SingleAsync(cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return GetCollection<TEntity>().AsQueryable();
        }

        #endregion IQuerier Members
    }
}