using Microsoft.AspNetCore.Mvc.Razor;

namespace Nameless.Skeleton.Framework.Web {

    /// <summary>
    /// Abstract implementation of <see cref="RazorPage{TModel}"/> to use
    /// the localization system.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ApplicationRazorPage<TModel> : RazorPage<TModel> {

        #region Public Properties

        /// <summary>
        /// Gets the current controller name
        /// </summary>
        public string CurrentControllerName {
            get { return (string)ViewContext.RouteData.Values["controller"]; }
        }

        /// <summary>
        /// Gets the current action name
        /// </summary>
        public string CurrentActionName {
            get { return (string)ViewContext.RouteData.Values["action"]; }
        }

        #endregion Public Properties
    }

    /// <summary>
    /// Dynamic model implementation of <see cref="RazorPage{TModel}"/>.
    /// </summary>
    public abstract class ApplicationRazorPage : ApplicationRazorPage<dynamic> {
    }
}