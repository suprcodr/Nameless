using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Nameless.Skeleton.Framework.Data.Generic {

    /// <summary>
    /// Implementation of <see cref="IRepository"/> for Entity Framework
    /// </summary>
    public class EntityFrameworkRepository : IRepository {

        #region Private Read-Only Fields

        private readonly DbContext _dbContext;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="EntityFrameworkRepository"/>
        /// </summary>
        /// <param name="dbContext">The entity framework database context.</param>
        public EntityFrameworkRepository(DbContext dbContext) {
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
            throw new NotImplementedException();
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            Prevent.ParameterNull(where, nameof(where));

            return _dbContext.Set<TEntity>().SingleOrDefault(where);
        }

        public TEntity FindOne<TEntity>(object id) where TEntity : class {
            return _dbContext.Set<TEntity>().SingleOrDefault(_ => Equals(IDAttribute.GetID(_), id));
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
    }
}