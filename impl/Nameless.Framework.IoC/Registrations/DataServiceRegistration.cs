using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Framework.Data.Generic;

namespace Nameless.Framework.IoC.Registrations {

    public sealed class DataServiceRegistration : ServiceRegistrationBase {

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

        #endregion Public Properties

        #region Public Constructors

        public DataServiceRegistration(Assembly[] supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Register() {
            Builder
                .RegisterType(GetRepositoryImplementation())
                .As<IRepository>()
                .SetLifetimeScope(RepositoryLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetRepositoryImplementation() {
            if (RepositoryImplementation != null) {
                return RepositoryImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IRepository)).SingleOrDefault();
        }

        #endregion Private Methods
    }
}