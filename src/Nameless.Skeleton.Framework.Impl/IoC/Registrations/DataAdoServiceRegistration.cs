using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Skeleton.Framework.Data.Ado;

namespace Nameless.Skeleton.Framework.IoC.Modules {

    /// <summary>
    /// Autofac module implementation for Nameless.Skeleton.Framework.Data namespace.
    /// </summary>
    public class DataAdoServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="IDatabase"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="Database"/>.</remarks>
        public Type DatabaseImplementation { get; set; } = typeof(Database);

        /// <summary>
        /// Gets or sets the <see cref="IDatabase"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType DatabaseLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

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
        /// Whether if will use transactions or not. Default is <c>false</c>.
        /// </summary>
        public bool UseTransaction { get; set; } = false;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DataAdoServiceRegistration"/>.
        /// </summary>
        public DataAdoServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="DataAdoServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public DataAdoServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            var databaseImplementation = GetDatabaseImplementation();
            if (UseTransaction) {
                Builder.RegisterType(databaseImplementation).Named<IDatabase>(databaseImplementation.FullName).SetLifetimeScope(DatabaseLifetimeScope);
                Builder.RegisterDecorator<IDatabase>(_ => new DatabaseWithTransaction(_), databaseImplementation.FullName);
            } else {
                Builder.RegisterType(databaseImplementation).As<IDatabase>().SetLifetimeScope(DatabaseLifetimeScope);
            }

            Builder.RegisterType(GetDbProviderImplementation()).As<IDbProvider>().SetLifetimeScope(DbProviderLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetDatabaseImplementation() {
            if (DatabaseImplementation != null) {
                return DatabaseImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IDatabase)).SingleOrDefault();
        }

        private Type GetDbProviderImplementation() {
            if (DbProviderImplementation != null) {
                return DbProviderImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IDbProvider)).SingleOrDefault();
        }

        #endregion Private Methods
    }
}