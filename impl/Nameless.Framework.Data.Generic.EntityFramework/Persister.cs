using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nameless.Framework.Data.Generic.Sql.EntityFramework {

    public sealed class Persister : IPersister {

        #region Public Static Read-Only Fields

        public static readonly int BatchSize = 1024;

        #endregion Public Static Read-Only Fields

        #region Private Read-Only Fields

        private readonly DbContext _dbContext;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Repository"/>
        /// </summary>
        /// <param name="dbContext">The entity framework database context.</param>
        public Persister(DbContext dbContext) {
            Prevent.ParameterNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        #endregion Public Constructors

        #region Private Methods

        private DbSet<TEntity> GetSet<TEntity>() where TEntity : class {
            return _dbContext.Set<TEntity>();
        }

        private void Commit() {
            _dbContext.SaveChanges();
        }

        #endregion Private Methods

        #region IPersister Members

        public Task SaveAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            progress = progress ?? NullProgress<int>.Instance;

            return Task.Run(() => {
                var counter = 0;
                var pageCount = (entities.Length / BatchSize) + 1;
                for (var page = 0; page < pageCount; page++) {
                    var items = entities.Skip(page * BatchSize).Take(BatchSize);
                    foreach (var item in items) {
                        progress.Report(++counter);
                        cancellationToken.ThrowIfCancellationRequested();

                        var entry = GetSet<TEntity>().Attach(item);
                        switch (entry.State) {
                            case EntityState.Detached:
                            case EntityState.Unchanged:
                                GetSet<TEntity>().Add(item);
                                break;

                            case EntityState.Modified:
                                GetSet<TEntity>().Update(item);
                                break;
                        }
                    }
                    Commit();
                }
            }, cancellationToken);
        }

        public Task DeleteAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class {
            progress = progress ?? NullProgress<int>.Instance;

            return Task.Run(() => {
                var counter = 0;
                var pageCount = (entities.Length / BatchSize) + 1;
                for (var page = 0; page < pageCount; page++) {
                    var items = entities.Skip(page * BatchSize).Take(BatchSize);
                    foreach (var item in items) {
                        progress.Report(++counter);
                        cancellationToken.ThrowIfCancellationRequested();

                        GetSet<TEntity>().Remove(item);
                    }
                    Commit();
                }
            }, cancellationToken);
        }

        #endregion IPersister Members
    }
}