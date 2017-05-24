using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Framework.IoC;

namespace Nameless.Framework.Services.IoC {

    /// <summary>
    /// Autofac module implementation for Nameless.Framework.Services namespace.
    /// </summary>
    public class ClockServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="IClock"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="Clock"/>.</remarks>
        public Type ClockImplementation { get; set; } = typeof(Clock);

        /// <summary>
        /// Gets or sets the <see cref="IClock"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType ClockLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ClockServiceRegistration"/>.
        /// </summary>
        public ClockServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ClockServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public ClockServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder.RegisterType(GetClockImplementation()).As<IClock>().SetLifetimeScope(ClockLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetClockImplementation() {
            if (ClockImplementation != null) {
                return ClockImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IClock)).SingleOrDefault();
        }

        #endregion Private Methods
    }
}