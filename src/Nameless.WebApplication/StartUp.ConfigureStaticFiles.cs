using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Methods

        private void ConfigureStaticFiles(IApplicationBuilder appBuilder) {
            Action<StaticFileResponseContext> CacheStaticFileResponseContextAction = _ => _.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=604800" /* One week (in seconds) */;
            appBuilder.UseResponseCompression()
                       .UseStaticFiles(new StaticFileOptions { // For the wwwroot folder
                           OnPrepareResponse = CacheStaticFileResponseContextAction
                       })
                       .UseStaticFiles(new StaticFileOptions { // For the bower_components folder
                           FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/lib")),
                           RequestPath = new PathString("/lib"),
                           OnPrepareResponse = CacheStaticFileResponseContextAction
                       })
                       .UseStaticFiles(new StaticFileOptions { // For the UserManagement Area
                           FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Areas/UserManagement/wwwroot")),
                           RequestPath = new PathString("/lib"),
                           OnPrepareResponse = CacheStaticFileResponseContextAction
                       });
        }

        #endregion Private Methods
    }
}