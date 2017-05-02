using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Skeleton.Framework.Data.Generic;

namespace Nameless.Skeleton.Framework.IoC.Modules {

    /// <summary>
    /// Autofac module implementation for Nameless.Skeleton.Framework.Data namespace.
    /// </summary>
    public class DataGenericServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="IRepository"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="EntityFrameworkRepository"/>.</remarks>
        public Type RepositoryImplementation { get; set; } = typeof(EntityFrameworkRepository);

        /// <summary>
        /// Gets or sets the <see cref="IRepository"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType RepositoryLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DataGenericServiceRegistration"/>.
        /// </summary>
        public DataGenericServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="DataGenericServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public DataGenericServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder.RegisterType(GetRepositoryImplementation()).As<IRepository>().SetLifetimeScope(RepositoryLifetimeScope);
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