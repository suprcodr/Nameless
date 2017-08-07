using Microsoft.Extensions.Logging;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Methods

        private void ConfigureLogging(ILoggerFactory factory) {
            factory.AddConsole(Configuration.GetSection("Logging"));
        }

        #endregion Private Methods
    }
}