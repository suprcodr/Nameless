using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Generic {

    /// <summary>
    /// Null Object Pattern implementation for IDirectiveExecutor. (see: https://en.wikipedia.org/wiki/Null_Object_pattern)
    /// </summary>
    public sealed class NullDirectiveExecutor : IDirectiveExecutor {

        #region Private Static Read-Only Fields

        private static readonly IDirectiveExecutor _instance = new NullDirectiveExecutor();

        #endregion Private Static Read-Only Fields

        #region Public Static Properties

        /// <summary>
        /// Gets the unique instance of NullDirectiveExecutor.
        /// </summary>
        public static IDirectiveExecutor Instance {
            get { return _instance; }
        }

        #endregion Public Static Properties

        #region Static Constructors

        // Explicit static constructor to tell the C# compiler
        // not to mark type as beforefieldinit
        static NullDirectiveExecutor() {
        }

        #endregion Static Constructors

        #region Private Constructors

        // Prevents the class from being constructed.
        private NullDirectiveExecutor() {
        }

        #endregion Private Constructors

        #region IDirectiveExecutor Members

        public Task<TResult> ExecuteDirectiveAsync<TResult, TDirective>(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) where TDirective : IDirective<TResult> {
            return Task.FromResult(default(TResult));
        }

        #endregion IDirectiveExecutor Members
    }
}