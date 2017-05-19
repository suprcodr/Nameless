using IAspNetHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Nameless.Skeleton.Framework.Services {

    public class HostingEnvironmentWrapper : IHostingEnvironment {

        #region Private Read-Only Fields

        private readonly IAspNetHostingEnvironment _hostingEnvironment;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public HostingEnvironmentWrapper(IAspNetHostingEnvironment hostingEnvironment) {
            Prevent.ParameterNull(hostingEnvironment, nameof(hostingEnvironment));

            _hostingEnvironment = hostingEnvironment;
        }

        #endregion Public Constructors

        #region IHostingEnvironment Members

        public string ApplicationName => _hostingEnvironment.ApplicationName;

        #endregion IHostingEnvironment Members
    }
}