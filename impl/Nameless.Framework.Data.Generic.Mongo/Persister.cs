using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Nameless.Framework.Data.Generic.NoSql.Mongo {

    public sealed class Persister : IPersister {

        #region Public Static Read-Only Fields

        public static readonly int BatchSize = 1024;

        #endregion Public Static Read-Only Fields

        #region Private Read-Only Fields

        private readonly IMongoDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public Persister(IMongoDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region Private Methods

        private IMongoCollection<TEntity> GetCollection<TEntity>() {
            return _database.GetCollection<TEntity>(typeof(TEntity).FullName);
        }

        #endregion Private Methods

        #region IPersister Members

        public Task SaveAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            var collection = GetCollection<TEntity>();
            var name = IDAttribute.GetName<TEntity>();

            progress = progress ?? NullProgress<int>.Instance;

            return Task.Run(() => {
                var counter = 0;
                var pageCount = (entities.Length / BatchSize) + 1;
                for (var page = 0; page < pageCount; page++) {
                    var items = entities.Skip(page * BatchSize).Take(BatchSize);
                    foreach (var item in items) {
                        progress.Report(++counter);
                        cancellationToken.ThrowIfCancellationRequested();

                        var id = IDAttribute.GetValue(item);
                        var queryID = Builders<TEntity>.Filter.Eq(name, id);

                        GetCollection<TEntity>().ReplaceOne(queryID, item, new UpdateOptions { IsUpsert = true });
                    }
                }
            }, cancellationToken);
        }

        public Task DeleteAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            var collection = GetCollection<TEntity>();
            var name = IDAttribute.GetName<TEntity>();

            progress = progress ?? NullProgress<int>.Instance;

            return Task.Run(() => {
                var counter = 0;
                var pageCount = (entities.Length / BatchSize) + 1;
                for (var page = 0; page < pageCount; page++) {
                    var items = entities.Skip(page * BatchSize).Take(BatchSize);
                    foreach (var item in items) {
                        progress.Report(++counter);
                        cancellationToken.ThrowIfCancellationRequested();

                        var id = IDAttribute.GetValue(item);
                        var queryID = Builders<TEntity>.Filter.Eq(name, id);

                        GetCollection<TEntity>().DeleteOne(queryID);
                    }
                }
            }, cancellationToken);
        }

        #endregion IPersister Members
    }
}