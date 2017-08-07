using Microsoft.AspNetCore.Builder;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Methods

        private void ConfigureIdentity(IApplicationBuilder appBuilder) {
            appBuilder.UseIdentity();
        }

        #endregion Private Methods
    }
}