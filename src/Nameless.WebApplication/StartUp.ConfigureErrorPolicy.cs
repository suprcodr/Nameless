using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Methods

        private void ConfigureErrorPolicy(IApplicationBuilder appBuilder, IHostingEnvironment environment) {
            if (environment.IsDevelopment()) {
                appBuilder.UseDeveloperExceptionPage();
            } else {
                appBuilder.UseExceptionHandler("/Home/Error");
            }
            appBuilder.UseStatusCodePagesWithRedirects("/Error/{0}");
        }

        #endregion Private Methods
    }
}