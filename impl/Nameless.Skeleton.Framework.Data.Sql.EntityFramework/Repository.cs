using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Nameless.Skeleton.Framework.Data.Sql.Common;

namespace Nameless.Skeleton.Framework.Data.Sql.EntityFramework {

    /// <summary>
    /// Implementation of <see cref="IRepository"/> for Entity Framework
    /// </summary>
    public sealed class Repository : IRepository, IDbConnectionAccessor {

        #region Private Read-Only Fields

        private readonly DbContext _dbContext;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Repository"/>
        /// </summary>
        /// <param name="dbContext">The entity framework database context.</param>
        public Repository(DbContext dbContext) {
            Prevent.ParameterNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        #endregion Public Constructors

        #region IRepository Members

        public dynamic ExecuteDirective<TDirective>(dynamic parameters) where TDirective : IDirective {
            Prevent.ParameterNull(parameters, nameof(parameters));

            if (!typeof(Directive).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive)}\"");
            }

            var directive = (IDirective)Activator.CreateInstance(typeof(TDirective), new object[] { _dbContext });

            return directive.Execute(parameters);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            Prevent.ParameterNull(where, nameof(where));

            return _dbContext.Set<TEntity>().SingleOrDefault(where);
        }

        public TEntity FindOne<TEntity>(object id) where TEntity : class {
            Prevent.ParameterNull(id, nameof(id));

            return _dbContext.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return _dbContext.Set<TEntity>();
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class {
            Prevent.ParameterNull(entity, nameof(entity));

            var entry = _dbContext.Set<TEntity>().Attach(entity);
            switch (entry.State) {
                case EntityState.Detached:
                case EntityState.Unchanged:
                    _dbContext.Set<TEntity>().Add(entity);
                    break;

                case EntityState.Modified:
                    _dbContext.Set<TEntity>().Update(entity);
                    break;
            }
            _dbContext.SaveChanges();
        }

        #endregion IRepository Members

        #region IDbConnectionAccessor Members

        public IDbConnection Connection {
            get { return _dbContext.Database.GetDbConnection(); }
        }

        #endregion IDbConnectionAccessor Members
    }
}