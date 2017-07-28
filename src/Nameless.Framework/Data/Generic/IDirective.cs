using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    /// <summary>
    /// Defines methods for directives.
    /// </summary>
    /// <typeparam name="TResult">Type of the result</typeparam>
	public interface IDirective<TResult> {

        #region Methods

        /// <summary>
        /// Executes the directive.
        /// </summary>
        /// <param name="parameters">The directive parameters.</param>
        /// <param name="cancellationToken">The cancellation token, if any.</param>
        /// <param name="progress">The progress notifier, if any.</param>
        /// <returns>A dynamic representing the directive execution.</returns>
        Task<TResult> ExecuteAsync(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null);

        #endregion Methods
    }
}