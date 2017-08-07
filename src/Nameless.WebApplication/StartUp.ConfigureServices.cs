using System;
using System.Globalization;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nameless.Framework.CQRS.IoC;
using Nameless.Framework.Data.Ado;
using Nameless.Framework.Data.Ado.IoC;
using Nameless.Framework.EventSourcing.IoC;
using Nameless.Framework.IoC;
using Nameless.Framework.Localization.IoC;
using Nameless.Framework.Logging.IoC;
using Nameless.Framework.ObjectMapper.IoC;
using Nameless.Framework.Services.IoC;
using Nameless.WebApplication.Code;
using Nameless.WebApplication.Core;
using Nameless.WebApplication.Core.Identity.Models;
using Nameless.WebApplication.Core.Identity.Stores;

namespace Nameless.WebApplication {

    public partial class StartUp {

        #region Private Fields

        private ICompositionRoot _compositionRoot;

        #endregion Private Fields

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            // Configure settings
            services.ConfigurePocoSettings<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddResponseCompression(options => {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
            });

            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMvc(opt => { opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); })
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
                Assembly.Load(new AssemblyName("Nameless.WebApplication")),
                Assembly.Load(new AssemblyName("Nameless.WebApplication.Core"))
            };

            _compositionRoot.Compose(new CQRSServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new AdoServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new EventSourcingServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new LocalizationServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new LoggingServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new ObjectMapperServiceRegistration(supportAssemblies));
            _compositionRoot.Compose(new ServicesServiceRegistration(supportAssemblies));
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