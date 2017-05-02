using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Nameless.Skeleton.Framework.Localization;
using Nameless.Skeleton.Framework.Logging;

namespace Nameless.Skeleton.Framework.Web {

    public abstract class ApplicationControllerBase : Controller {

        #region Private Read-Only Fields

        private IStringLocalizer _localizer;
        private ILogger _logger;

        #endregion Private Read-Only Fields

        #region Public Properties
        
        public IStringLocalizer T {
            get { return _localizer ?? NullStringLocalizer.Instance; }
            set { _localizer = value ?? NullStringLocalizer.Instance; }
        }

        public ILogger Logger {
            get { return _logger ?? NullLogger.Instance; }
            set { _logger = value ?? NullLogger.Instance; }
        }

        #endregion Public Properties
    }
}