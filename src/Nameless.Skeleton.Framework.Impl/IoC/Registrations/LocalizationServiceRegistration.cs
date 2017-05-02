using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Localization;
using Nameless.Skeleton.Framework.Localization;

namespace Nameless.Skeleton.Framework.IoC.Modules {

    /// <summary>
    /// Autofac module implementation for Nameless.Skeleton.Framework.Localization namespace.
    /// </summary>
    public sealed class LocalizationServiceRegistration : ServiceRegistrationBase {

        #region Private Static Read-Only Fields

        private static readonly ConcurrentDictionary<string, IStringLocalizer> Cache = new ConcurrentDictionary<string, IStringLocalizer>();

        #endregion Private Static Read-Only Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="IPluralStringLocalizer{T}"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="PluralStringLocalizer{T}"/>.</remarks>
        public Type PluralStringLocalizerImplementation { get; set; } = typeof(IPluralStringLocalizer<>);

        /// <summary>
        /// Gets or sets the <see cref="IPluralStringLocalizer{T}"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.Singleton"/>.</remarks>
        public LifetimeScopeType PluralStringLocalizerLifetimeScope { get; set; } = LifetimeScopeType.Singleton;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="LocalizationServiceRegistration"/>.
        /// </summary>
        public LocalizationServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="LocalizationServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public LocalizationServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder
                .RegisterTypes(new[] { GetPluralStringLocalizerImplementation() })
                .OnRegistered(AttachToComponentRegistration)
                .AsClosedTypesOf(typeof(IPluralStringLocalizer<>))
                .SetLifetimeScope(PluralStringLocalizerLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private void AttachToComponentRegistration(ComponentRegisteredEventArgs args) {
            var registration = args.ComponentRegistration;
            var userProperty = FindUserProperty(registration.Activator.LimitType);

            if (userProperty != null) {
                var scope = registration.Activator.LimitType;

                registration.Activated += (sender, e) => {
                    if (e.Instance.GetType() != scope) {
                        return;
                    }

                    var localizer = Cache.GetOrAdd(scope.FullName, key => ResolveLocalizer(e.Context, scope));

                    userProperty.SetValue(e.Instance, localizer, null);
                };
            }
        }

        private Type GetPluralStringLocalizerImplementation() {
            if (PluralStringLocalizerImplementation != null) {
                return PluralStringLocalizerImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IPluralStringLocalizer<>)).SingleOrDefault();
        }

        #endregion Private Methods

        #region Private Static Methods

        private static PropertyInfo FindUserProperty(Type type) {
            return type.GetProperty("T", typeof(IStringLocalizer));
        }

        private static IStringLocalizer ResolveLocalizer(IComponentContext context, Type scope) {
            return context.Resolve<IStringLocalizerFactory>().Create(scope);
        }

        #endregion Private Static Methods
    }
}