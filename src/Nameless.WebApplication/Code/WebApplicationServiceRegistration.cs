using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Nameless.Framework.IoC;
using Nameless.WebApplication.Core;

namespace Nameless.WebApplication.Code {

    public class WebApplicationServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        public ControllerFeature Feature { get; set; }
        public IServiceCollection Services { get; set; }

        #endregion Public Properties

        #region Public Override Methods

        public override void Register() {
            Builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            Builder
                .RegisterTypes(Feature.Controllers.Select(_ => _.AsType()).ToArray())
                .PropertiesAutowired();

            Builder
                .RegisterAssemblyTypes(SupportAssemblies)
                .Where(_ => _.GetTypeInfo().GetCustomAttribute<PropertyAutoWireAttribute>(inherit: true) != null)
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            Builder.RegisterInstance(NullCommunicationService.Instance).As<ICommunicationService>().SingleInstance();
            Builder.RegisterInstance(NullApplicationContext.Instance).As<IApplicationContext>().SingleInstance();
            

            Builder.Populate(Services);
        }

        #endregion Public Override Methods
    }
}