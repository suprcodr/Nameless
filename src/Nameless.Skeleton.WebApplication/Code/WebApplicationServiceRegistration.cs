﻿using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Nameless.Skeleton.Framework.IoC;
using Nameless.Skeleton.Framework.Web;

namespace Nameless.Skeleton.WebApplication.Code {

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

            Builder.Populate(Services);
        }

        #endregion Public Override Methods
    }
}