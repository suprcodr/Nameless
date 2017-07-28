using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Nameless.Framework.IoC;

namespace Nameless.Framework.Logging.IoC {

    /// <summary>
    /// Autofac module for implementations from Nameless.Framework.Logging namespace.
    /// </summary>
    public sealed class LoggingServiceRegistration : ServiceRegistrationBase {

        #region Private Read-Only Fields

        private readonly ConcurrentDictionary<string, ILogger> _cache = new ConcurrentDictionary<string, ILogger>();

        #endregion Private Read-Only Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="ILoggerFactory"/> implementation.
        /// </summary>
        public Type LoggerFactoryImplementation { get; set; } = typeof(LoggerFactory);

        /// <summary>
        /// Gets or sets the <see cref="ILoggerFactory"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType LoggerFactoryLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="ILogger"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.Transient"/>.</remarks>
        public LifetimeScopeType LoggerLifetimeScope { get; set; } = LifetimeScopeType.Transient;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="LoggingServiceRegistration"/>
        /// </summary>
        public LoggingServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="LoggingServiceRegistration"/>
        /// </summary>
        /// <param name="supportAssemblies">Support assemblies.</param>
        public LoggingServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder
                .RegisterType(GetLoggerFactoryImplementation())
                .OnRegistered(AttachToComponentRegistration)
                .As<ILoggerFactory>()
                .SetLifetimeScope(LoggerFactoryLifetimeScope);

            Builder
                .Register(CreateLogger)
                .As<ILogger>()
                .SetLifetimeScope(LoggerLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private void AttachToComponentRegistration(ComponentRegisteredEventArgs componentArgs) {
            var registration = componentArgs.ComponentRegistration;
            var implementationType = registration.Activator.LimitType;

            // verify if the implementation type needs logger injection via constructor.
            var constructorNeedLogger = implementationType.GetConstructors()
                .Any(constructor => constructor.GetParameters()
                    .Any(parameter => parameter.ParameterType == typeof(ILogger)));

            // if need, inject and return.
            if (constructorNeedLogger) {
                registration.Preparing += (sender, args) => {
                    var logger = GetCachedLogger(implementationType, args.Context);
                    var parameter = new TypedParameter(typeof(ILogger), logger);
                    args.Parameters = args.Parameters.Concat(new[] { parameter });
                };

                return;
            }

            // build an array of actions on this type to assign loggers to member properties
            var propertyInjectorCollection = BuildPropertyInjectorCollection(implementationType).ToArray();

            // otherwise, whan an instance of this component is activated, inject the loggers on the instance
            registration.Activated += (sender, e) => {
                foreach (var injector in propertyInjectorCollection) {
                    injector(e.Context, e.Instance);
                }
            };
        }

        #endregion Private Methods

        #region Private Static Methods

        private static ILogger CreateLogger(IComponentContext context, IEnumerable<Parameter> parameters) {
            Type type;

            // TODO: Remove this hack
            try { type = parameters.TypedAs<Type>(); } catch { type = ((NamedParameter)parameters.First()).Value.GetType(); }

            return context.Resolve<ILoggerFactory>().CreateLogger(type);
        }

        #endregion Private Static Methods

        #region Private Methods

        private Type GetLoggerFactoryImplementation() {
            if (LoggerFactoryImplementation != null) {
                return LoggerFactoryImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(ILoggerFactory)).SingleOrDefault();
        }

        private IEnumerable<Action<IComponentContext, object>> BuildPropertyInjectorCollection(Type componentType) {
            // Look for settable properties of type "ILogger"
            var properties = componentType
                .GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)
                .Select(property => new {
                    PropertyInfo = property,
                    property.PropertyType,
                    IndexParameters = property.GetIndexParameters().ToArray(),
                    Accessors = property.GetAccessors(false)
                })
                .Where(property => property.PropertyType == typeof(ILogger)) // must be a logger
                .Where(property => property.IndexParameters.Length == 0) // must not be an indexer
                .Where(property => property.Accessors.Length != 1 || property.Accessors[0].ReturnType == typeof(void)); //must have get/set, or only set

            // Return an array of actions that resolve a logger and assign the property
            foreach (var entry in properties) {
                var property = entry.PropertyInfo;

                yield return (context, instance) => {
                    var logger = GetCachedLogger(componentType, context);
                    property.SetValue(instance, logger, null);
                };
            }
        }

        private ILogger GetCachedLogger(Type componentType, IComponentContext context) {
            return _cache.GetOrAdd(
                key: componentType.ToString(),
                valueFactory: value => context.Resolve<ILogger>(new TypedParameter(typeof(Type), componentType)));
        }

        #endregion Private Methods
    }
}