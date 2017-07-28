using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Framework.IoC;

namespace Nameless.Framework.Data.Generic.Sql.EntityFramework.IoC {

    /// <summary>
    /// Service registration for implementation of EntityFramework <see cref="IRepository"/>
    /// </summary>
    public class EntityFrameworkServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

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

        public EntityFrameworkServiceRegistration(Assembly[] supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Register() {
            Builder
                .RegisterType(GetPersisterImplementation())
                .As<IPersister>()
                .SetLifetimeScope(LifetimeScope);

            Builder
                .RegisterType(GetQuerierImplementation())
                .As<IQuerier>()
                .SetLifetimeScope(LifetimeScope);

            Builder
                .RegisterType(GetDirectiveExecutorImplementation())
                .As<IDirectiveExecutor>()
                .SetLifetimeScope(LifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}