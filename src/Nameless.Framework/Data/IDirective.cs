using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data {

    /// <summary>
    /// Defines methods for directives.
    /// </summary>
	public interface IDirective {

        #region Methods

        /// <summary>
        /// Executes the directive.
        /// </summary>
        /// <returns>A dynamic representing the directive execution.</returns>
        Task<dynamic> ExecuteAsync(dynamic parameters, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Methods
    }
}