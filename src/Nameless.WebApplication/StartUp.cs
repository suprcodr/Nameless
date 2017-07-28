using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.IoC;
using Nameless.Framework.IoC.Modules;
using Nameless.Framework.Web;
using Nameless.Framework.Web.Identity.Models;
using Nameless.Framework.Web.Identity.Stores;
using Nameless.WebApplication.Code;

namespace Nameless.WebApplication {

    public class StartUp {

        #region Private Fields

        private ICompositionRoot _compositionRoot;

        #endregion Private Fields

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
                builder.AddUserSecrets(typeof(StartUp).GetTypeInfo().Assembly);
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        #endregion Public Constructors

        #region Public Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder application, IHostingEnvironment environment, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (environment.IsDevelopment()) {
                application.UseDeveloperExceptionPage();
            } else {
                application.UseExceptionHandler("/Home/Error");
            }

            // application.UseStatusCodePagesWithRedirects("/Error/{0}");

            Action<StaticFileResponseContext> CacheStaticFileResponseContextAction = _ => _.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=604800" /* One week (in seconds) */;
            application.UseResponseCompression()
                       .UseStaticFiles(new StaticFileOptions { // For the wwwroot folder
                           OnPrepareResponse = CacheStaticFileResponseContextAction
                       })
                       .UseStaticFiles(new StaticFileOptions { // For the bower_components folder
                           FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/lib")),
                           RequestPath = new PathString("/lib"),
                           OnPrepareResponse = CacheStaticFileResponseContextAction
                       });
                       //.UseStaticFiles(new StaticFileOptions { // For the Bookshelf folder
                       //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Areas/Bookshelf/wwwroot")),
                       //    RequestPath = new PathString("/bookshelf"),
                       //    OnPrepareResponse = CacheStaticFileResponseContextAction
                       //});
            application.UseRequestLocalization(Configuration.Get<RequestLocalizationOptions>());

            application.UseIdentity();

            application.UseMvc(routes => {
                routes.MapRoute(
                    name: "bookshelfRoute",
                    template: "Bookshelf",
                    defaults: new { area = "Bookshelf", controller = "Book", action = "Search" });

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            applicationLifetime.ApplicationStopped.Register(() => _compositionRoot.TearDown());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            // Configure settings
            services.ConfigurePocoSettings<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddResponseCompression(options => {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });

            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMvc()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => {
                        options.ResourcesPath = "Resources";
                    })
                    .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new[] {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture("pt-BR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddIdentity<User, Role>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            var manager = new ApplicationPartManager();

            manager.ApplicationParts.Add(new AssemblyPart(typeof(StartUp).GetTypeInfo().Assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());

            var feature = new ControllerFeature();

            manager.PopulateFeature(feature);

            _compositionRoot = new CompositionRoot();
            var supportAssemblies = new[] {
                Assembly.Load(new AssemblyName("Nameless.Common")),
                Assembly.Load(new AssemblyName("Nameless.Framework")),
                Assembly.Load(new AssemblyName("Nameless.Framework.Impl")),
                Assembly.Load(new AssemblyName("Nameless.Framework.Web")),
                Assembly.Load(new AssemblyName("Nameless.WebApplication"))
            };

            _compositionRoot.Compose(new CqrsServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new AdoServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new EventSourcingServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new LocalizationServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new Log4NetServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new AutoMapperServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new ClockServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new WebApplicationServiceRegistration {
                Feature = feature,
                Services = services
            });

            _compositionRoot.StartUp();

            return new AutofacServiceProvider(((CompositionRoot)_compositionRoot).Container);
        }

        #endregion Public Methods
    }
}