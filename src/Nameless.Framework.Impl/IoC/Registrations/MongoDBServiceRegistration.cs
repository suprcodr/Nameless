using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Nameless.Framework.Data;
using Nameless.Framework.Data.NoSql.Mongo;

namespace Nameless.Framework.IoC.Registrations {

    public sealed class MongoDBServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the implementations of <see cref="MappingRegistrationBase"/>.
        /// </summary>
        public Type[] MappingRegistrationImplementations { get; set; } = Type.EmptyTypes;

        /// <summary>
        /// Gets or sets the <see cref="IRepository"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="Repository"/>.</remarks>
        public Type RepositoryImplementation { get; set; } = typeof(Repository);

        /// <summary>
        /// Gets or sets the <see cref="IRepository"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType RepositoryLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        #endregion Public Properties

        #region Public Constructors

        public MongoDBServiceRegistration(Assembly[] supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Register() {
            RegisterMappings();

            Builder
                .RegisterType<Repository>()
                .As<IRepository>()
                .OnPreparing(Preparing)
                .SetLifetimeScope(RepositoryLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private void RegisterMappings() {
            foreach (var mappingRegistrationType in GetMappingRegistrationImplementations()) {
                var mappingRegistration = (MappingRegistrationBase)Activator.CreateInstance(mappingRegistrationType);
                var mapping = mappingRegistration.Create();

                BsonClassMap.RegisterClassMap(mapping);
            }
        }

        private void Preparing(PreparingEventArgs args) {
            var settings = args.Context.Resolve<MongoDbSettings>();
            var client = new MongoClient(settings.ConnectionString);
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

        private Type GetRepositoryImplementation() {
            if (RepositoryImplementation != null) {
                return RepositoryImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IRepository)).SingleOrDefault();
        }

        #endregion Private Methods
    }
}