using System;
using System.Linq;
using System.Linq.Expressions;

namespace Nameless.Skeleton.Framework.Data {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="IRepository"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public class NullRepository : IRepository {

        #region Public Static Fields

        /// <summary>
        /// Gets the static current instance of <see cref="NullRepository"/>.
        /// </summary>
        public static readonly IRepository Instance = new NullRepository();

        #endregion Public Static Fields

        #region Private Constructors

        // Block construction of NullLogger
        private NullRepository() { }

        #endregion Private Constructors

        #region IRepository Members

        public void Delete<TEntity>(TEntity entity) where TEntity : class {
        }

        public dynamic ExecuteDirective<TDirective>(dynamic parameters) where TDirective : IDirective {
            return null;
        }

        public TEntity FindOne<TEntity>(object id) where TEntity : class {
            return default(TEntity);
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            return default(TEntity);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return Enumerable.Empty<TEntity>().AsQueryable();
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class {
        }

        #endregion IRepository Members
    }
}