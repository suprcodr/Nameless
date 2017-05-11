using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using AutoMapper;

namespace Nameless.Skeleton.Framework.IoC.Modules {

    /// <summary>
    /// Autofac module implementation for Nameless.Skeleton.Framework.ObjectMapper assembly.
    /// </summary>
    public class AutoMapperServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="ObjectMapper.IMapper"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="ObjectMapper.Mapper"/>.</remarks>
        public Type MapperImplementation { get; set; } = typeof(ObjectMapper.Mapper);

        /// <summary>
        /// Gets or sets the <see cref="ObjectMapper.IMapper"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType MapperLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="Profile"/> implementations.
        /// </summary>
        public Type[] ProfileImplementations { get; set; } = Type.EmptyTypes;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="AutoMapperServiceRegistration"/>.
        /// </summary>
        public AutoMapperServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="AutoMapperServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public AutoMapperServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder
                .RegisterType(GetMapperImplementation())
                .As<ObjectMapper.IMapper>()
                .OnPreparing(OnPreparingMapper)
                .SetLifetimeScope(MapperLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetMapperImplementation() {
            if (MapperImplementation != null) {
                return MapperImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(ObjectMapper.IMapper)).SingleOrDefault();
        }

        private Type[] GetProfileImplementations() {
            if (!ProfileImplementations.IsNullOrEmpty()) {
                return ProfileImplementations;
            }

            return GetImplementationsFromSupportAssemblies(typeof(Profile));
        }

        private void OnPreparingMapper(PreparingEventArgs args) {
            var profiles = GetProfileImplementations();

            args.Parameters = args.Parameters.Union(new[] {
                new ResolvedParameter(
                    (parameter, ctx) => parameter.ParameterType == typeof(IEnumerable<Profile>),
                    (parameter, ctx) => profiles.Select(_ => (Profile)Activator.CreateInstance(_))
                )
            });
        }

        #endregion Private Methods
    }
}