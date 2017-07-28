using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    public interface IPersister {

        #region Methods

        Task SaveAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class;

        Task DeleteAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null, params TEntity[] entities) where TEntity : class;

        #endregion Methods
    }
}