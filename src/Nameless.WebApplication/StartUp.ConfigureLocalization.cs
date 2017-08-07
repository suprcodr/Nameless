using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Methods

        private void ConfigureLocalization(IApplicationBuilder appBuilder) {
            appBuilder.UseRequestLocalization(Configuration.Get<RequestLocalizationOptions>());
        }

        #endregion Private Methods
    }
}