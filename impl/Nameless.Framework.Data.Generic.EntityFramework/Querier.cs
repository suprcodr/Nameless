using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nameless.Framework.Data.Generic.Sql.EntityFramework {

    public sealed class Querier : IQuerier {

        #region Private Read-Only Fields

        private readonly DbContext _dbContext;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Repository"/>
        /// </summary>
        /// <param name="dbContext">The entity framework database context.</param>
        public Querier(DbContext dbContext) {
            Prevent.ParameterNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        #endregion Public Constructors

        #region Private Methods

        private DbSet<TEntity> GetSet<TEntity>() where TEntity : class {
            return _dbContext.Set<TEntity>();
        }

        #endregion Private Methods

        #region IQuerier Members

        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.Run(() => GetSet<TEntity>().Where(expression).AsEnumerable());
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return GetSet<TEntity>().FindAsync(id, cancellationToken);
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return GetSet<TEntity>().SingleOrDefaultAsync(expression, cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return GetSet<TEntity>();
        }

        #endregion IQuerier Members
    }
}