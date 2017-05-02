using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nameless.Skeleton.Framework.Web {

    public static class ServiceCollectionExtension {

        #region Public Static Methods

        public static IServiceCollection ConfigurePocoSettings<TPocoSettings>(this IServiceCollection services, IConfiguration configuration, TPocoSettings settings) where TPocoSettings : class {
            Prevent.ParameterNull(services, nameof(services));
            Prevent.ParameterNull(configuration, nameof(configuration));
            Prevent.ParameterNull(settings, nameof(settings));

            configuration.Bind(settings);
            services.AddSingleton(settings);

            return services;
        }

        public static IServiceCollection ConfigurePocoSettings<TPocoSettings>(this IServiceCollection services, IConfiguration configuration) where TPocoSettings : class, new() {
            return ConfigurePocoSettings(services, configuration, new TPocoSettings());
        }

        public static IServiceCollection ConfigurePocoSettings<TPocoSettings>(this IServiceCollection services, IConfiguration configuration, Func<TPocoSettings> factory) where TPocoSettings : class {
            Prevent.ParameterNull(factory, nameof(factory));

            return ConfigurePocoSettings(services, configuration, factory());
        }

        #endregion Public Static Methods
    }
}