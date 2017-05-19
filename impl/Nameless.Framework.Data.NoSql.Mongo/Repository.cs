using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public void Delete<TEntity>(TEntity entity) where TEntity : class {
            var collection = GetCollection<TEntity>();
            var name = IDAttribute.GetName<TEntity>();
            var id = IDAttribute.GetValue(entity);
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            GetCollection<TEntity>().DeleteOne(queryID);
        }

        public dynamic ExecuteDirective<TDirective>(dynamic parameters) where TDirective : IDirective {
            if (!typeof(Directive).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive)}\"");
            }

            var directive = (IDirective)Activator.CreateInstance(typeof(TDirective), new object[] { _database });

            return directive.Execute(parameters);
        }

        public TEntity FindOne<TEntity>(object id) where TEntity : class {
            var name = IDAttribute.GetName<TEntity>();
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            return GetCollection<TEntity>().Find(queryID).SingleOrDefault();
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            return GetCollection<TEntity>().Find(where).SingleOrDefault();
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return GetCollection<TEntity>().AsQueryable();
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class {
            var collection = GetCollection<TEntity>();
            var name = IDAttribute.GetName<TEntity>();
            var id = IDAttribute.GetValue(entity);
            var queryID = Builders<TEntity>.Filter.Eq(name, id);

            GetCollection<TEntity>().ReplaceOne(queryID, entity, new UpdateOptions { IsUpsert = true });
        }

        #endregion IRepository Members
    }
}