using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Nameless.Framework.Data.Generic;
using Nameless.Framework.Data.Generic.Mongo;

namespace Nameless.Framework.IoC.Registrations {

    public sealed class MongoServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the implementations of <see cref="MappingRegistrationBase"/>.
        /// </summary>
        public Type[] MappingRegistrationImplementations { get; set; } = Type.EmptyTypes;

        /// <summary>
        /// Gets or sets the <see cref="IPersister"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="Persister"/>.</remarks>
        public Type PersisterImplementation { get; set; } = typeof(Persister);

        /// <summary>
        /// Gets or sets the <see cref="IQuerier"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="Querier"/>.</remarks>
        public Type QuerierImplementation { get; set; } = typeof(Querier);

        /// <summary>
        /// Gets or sets the <see cref="IDirectiveExecutor"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="DirectiveExecutor"/>.</remarks>
        public Type DirectiveExecutorImplementation { get; set; } = typeof(DirectiveExecutor);

        /// <summary>
        /// Gets or sets the infrastructure <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType LifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        #endregion Public Properties

        #region Public Constructors

        public MongoServiceRegistration(Assembly[] supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Register() {
            RegisterMappings();

            Builder
                .RegisterType<MongoClient>()
                .As<IMongoClient>()
                .OnPreparing(MongoClientPreparing)
                .SetLifetimeScope(LifetimeScope);
            
            Builder
                .RegisterType(GetPersisterImplementation())
                .As<IPersister>()
                .OnPreparing(InfrastructurePreparing)
                .SetLifetimeScope(LifetimeScope);

            Builder
                .RegisterType(GetQuerierImplementation())
                .As<IQuerier>()
                .OnPreparing(InfrastructurePreparing)
                .SetLifetimeScope(LifetimeScope);

            Builder
                .RegisterType(GetDirectiveExecutorImplementation())
                .As<IDirectiveExecutor>()
                .OnPreparing(InfrastructurePreparing)
                .SetLifetimeScope(LifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private void MongoClientPreparing(PreparingEventArgs args) {
            var settings = args.Context.Resolve<DatabaseSettings>();

            args.Parameters = args.Parameters.Union(new[] {
                new NamedParameter("connectionString", settings.ConnectionString)
            });
        }

        private void InfrastructurePreparing(PreparingEventArgs args) {
            var settings = args.Context.Resolve<DatabaseSettings>();
            var client = args.Context.Resolve<IMongoClient>();
            var database = client.GetDatabase(settings.DatabaseName);

            args.Parameters = args.Parameters.Union(new[] {
                TypedParameter.From(database)
            });
        }

        private Type[] GetMappingRegistrationImplementations() {
            if (!MappingRegistrationImplementations.IsNullOrEmpty()) {
                return MappingRegistrationImplementations;
            }

            return GetImplementationsFromSupportAssemblies(typeof(MappingRegistrationBase));
        }

        private Type GetPersisterImplementation() {
            if (PersisterImplementation != null) {
                return PersisterImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IPersister)).SingleOrDefault();
        }

        private Type GetQuerierImplementation() {
            if (QuerierImplementation != null) {
                return QuerierImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IQuerier)).SingleOrDefault();
        }

        private Type GetDirectiveExecutorImplementation() {
            if (DirectiveExecutorImplementation != null) {
                return DirectiveExecutorImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IDirectiveExecutor)).SingleOrDefault();
        }

        private void RegisterMappings() {
            foreach (var mappingRegistrationType in GetMappingRegistrationImplementations()) {
                var mappingRegistration = (MappingRegistrationBase)Activator.CreateInstance(mappingRegistrationType);
                var mapping = mappingRegistration.Create();

                BsonClassMap.RegisterClassMap(mapping);
            }
        }

        #endregion Private Methods
    }
}