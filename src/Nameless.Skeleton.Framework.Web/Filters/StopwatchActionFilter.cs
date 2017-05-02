using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nameless.Skeleton.Framework.Web.Filters {

    /// <summary>
    /// Implementation of <see cref="IActionFilter"/> to use <see cref="Stopwatch"/>.
    /// </summary>
    public class StopwatchActionFilter : IActionFilter {

        #region Private Read-Only Fields

        private readonly Stopwatch _stopwatch;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="StopwatchActionFilter"/>.
        /// </summary>
        public StopwatchActionFilter() {
            _stopwatch = Stopwatch.StartNew();
        }

        #endregion Public Constructors

        #region IActionFilter Members

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext filterContext) {
            _stopwatch.Stop();

            var httpContext = filterContext.HttpContext;
            var response = httpContext.Response;

            response.Headers.Add("X-Stopwatch", $"{_stopwatch.ElapsedMilliseconds} ms");
        }

        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            _stopwatch.Start();
        }

        #endregion IActionFilter Members
    }
}