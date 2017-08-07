using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Public Properties

        public IConfigurationRoot Configuration { get; }

        #endregion Public Properties

        #region Public Constructors

        public StartUp(IHostingEnvironment environment) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"AppSettings.{environment.EnvironmentName}.json", optional: true);

            if (environment.IsDevelopment()) {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<StartUp>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        #endregion Public Constructors

        #region Public Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appBuilder, IHostingEnvironment environment, ILoggerFactory loggerFactory) {
            ConfigureLogging(loggerFactory);
            ConfigureErrorPolicy(appBuilder, environment);
            ConfigureStaticFiles(appBuilder);
            ConfigureLocalization(appBuilder);
            ConfigureIdentity(appBuilder);
            ConfigureAuth(appBuilder);
            ConfigureMvc(appBuilder);
        }

        #endregion Public Methods
    }
}