using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nameless.Framework.Data.Generic.Sql.EntityFramework {

    public sealed class DirectiveExecutor : IDirectiveExecutor {

        #region Private Read-Only Fields

        private readonly DbContext _dbContext;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Repository"/>
        /// </summary>
        /// <param name="dbContext">The entity framework database context.</param>
        public DirectiveExecutor(DbContext dbContext) {
            Prevent.ParameterNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        #endregion Public Constructors

        #region IDirectiveExecutor Members

        public Task<TResult> ExecuteDirectiveAsync<TResult, TDirective>(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) where TDirective : IDirective<TResult> {
            if (!typeof(Directive<>).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive<>)}\"");
            }

            var directive = (IDirective<TResult>)Activator.CreateInstance(typeof(TDirective), new object[] { _dbContext });

            return directive.ExecuteAsync(parameters, cancellationToken, progress ?? NullProgress<int>.Instance);
        }

        #endregion IDirectiveExecutor Members
    }
}