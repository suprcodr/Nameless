using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    /// <summary>
    /// Null Object Pattern implementation for IQuerier. (see: https://en.wikipedia.org/wiki/Null_Object_pattern)
    /// </summary>
    public sealed class NullQuerier : IQuerier {

        #region Private Static Read-Only Fields

        private static readonly IQuerier _instance = new NullQuerier();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of NullQuerier.
        /// </summary>
        public static IQuerier Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static NullQuerier() {
        }

        #endregion Static Constructors

        #region Private Constructors

        // Prevents the class from being constructed.
        private NullQuerier() {
        }

        #endregion Private Constructors

        #region IQuerier Members

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(default(TEntity));
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(default(TEntity));
        }

        public Task<IEnumerable<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.FromResult(Enumerable.Empty<TEntity>());
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            return Enumerable.Empty<TEntity>().AsQueryable();
        }

        #endregion IQuerier Members
    }
}