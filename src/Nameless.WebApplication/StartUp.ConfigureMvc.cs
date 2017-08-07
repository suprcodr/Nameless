using Microsoft.AspNetCore.Builder;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Methods

        private void ConfigureMvc(IApplicationBuilder appBuilder) {
            appBuilder.UseMvc(routes => {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion Private Methods
    }
}