using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Skeleton.Framework.IoC;
using TransactionRepository = Nameless.Skeleton.Framework.Data.Sql.Repository;

namespace Nameless.Skeleton.Framework.Data.Sql.Ado.IoC {

    /// <summary>
    /// Service registration for ADO data access.
    /// </summary>
    public class AdoServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

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

        /// <summary>
        /// Gets or sets the <see cref="IDbProvider"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="DbProvider"/>.</remarks>
        public Type DbProviderImplementation { get; set; } = typeof(DbProvider);

        /// <summary>
        /// Gets or sets the <see cref="IDbProvider"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType DbProviderLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="IActionInformationExtractorFactory"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="ActionInformationExtractorFactory"/>.</remarks>
        public Type ActionInformationExtractorFactoryImplementation { get; set; } = typeof(ActionInformationExtractorFactory);

        /// <summary>
        /// Gets or sets the <see cref="IActionInformationExtractorFactory"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType ActionInformationExtractorFactoryLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="IActionInformationExtractor{TEntity}"/> implementations.
        /// </summary>
        public Type[] ActionInformationExtractorImplementations { get; set; } = Type.EmptyTypes;

        /// <summary>
        /// Whether if will use transactions or not. Default is <c>false</c>.
        /// </summary>
        public bool UseTransaction { get; set; } = false;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="AdoServiceRegistration"/>.
        /// </summary>
        public AdoServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AdoServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public AdoServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            var repositoryImplementation = GetRepositoryImplementation();
            if (UseTransaction) {
                Builder.RegisterType(repositoryImplementation).Named<IRepository>(repositoryImplementation.FullName).SetLifetimeScope(RepositoryLifetimeScope);
                Builder.RegisterDecorator<IRepository>(_ => new TransactionRepository(_), repositoryImplementation.FullName);
            } else {
                Builder.RegisterType(repositoryImplementation).As<IRepository>().SetLifetimeScope(RepositoryLifetimeScope);
            }

            Builder.RegisterType(GetDbProviderImplementation()).As<IDbProvider>().SetLifetimeScope(DbProviderLifetimeScope);

            Builder.RegisterType(GetActionInformationExtractorFactoryImplementation()).As<IActionInformationExtractorFactory>().SetLifetimeScope(ActionInformationExtractorFactoryLifetimeScope);
            Builder.RegisterTypes(GetActionInformationExtractorImplementations()).AsClosedTypesOf(typeof(IActionInformationExtractor<>)).SetLifetimeScope(RepositoryLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetRepositoryImplementation() {
            if (RepositoryImplementation != null) {
                return RepositoryImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IRepository)).SingleOrDefault();
        }

        private Type GetDbProviderImplementation() {
            if (DbProviderImplementation != null) {
                return DbProviderImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IDbProvider)).SingleOrDefault();
        }

        private Type GetActionInformationExtractorFactoryImplementation() {
            if (ActionInformationExtractorFactoryImplementation != null) {
                return ActionInformationExtractorFactoryImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IActionInformationExtractorFactory)).SingleOrDefault();
        }

        private Type[] GetActionInformationExtractorImplementations() {
            if (!ActionInformationExtractorImplementations.IsNullOrEmpty()) {
                return ActionInformationExtractorImplementations;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IActionInformationExtractor<>));
        }

        #endregion Private Methods
    }
}