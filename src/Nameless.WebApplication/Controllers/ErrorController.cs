using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nameless.Framework.Logging;
using Nameless.Framework.Web;

namespace Nameless.WebApplication.Controllers {

    public class ErrorController : ApplicationControllerBase {

        #region Public Methods

        [HttpGet("/Error/{statusCode}")]
        public async Task<IActionResult> Index(int statusCode) {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (reExecute != null) {
                Logger.Information($"Unexpected status code: {statusCode}, originalPath: {reExecute.OriginalPath}");
            } else {
                Logger.Information($"Unexpected status code: {statusCode}");
            }

            return View(statusCode);
        }

        #endregion Public Methods
    }
}