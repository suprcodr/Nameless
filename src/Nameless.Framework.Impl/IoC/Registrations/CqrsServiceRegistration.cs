using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Cqrs.Query;

namespace Nameless.Framework.IoC.Modules {

    /// <summary>
    /// Autofac module implementation for Nameless.Framework.Cqrs namespace.
    /// </summary>
    public class CqrsServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="ICommandDispatcher"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="CommandDispatcher"/>.</remarks>
        public Type CommandDispatcherImplementation { get; set; } = typeof(CommandDispatcher);

        /// <summary>
        /// Gets or sets the <see cref="ICommandDispatcher"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType CommandDispatcherLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="IQueryDispatcher"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="QueryDispatcher"/>.</remarks>
        public Type QueryDispatcherImplementation { get; set; } = typeof(QueryDispatcher);

        /// <summary>
        /// Gets or sets the <see cref="IQueryDispatcher"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType QueryDispatcherLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="ICommandHandler{TCommand}"/> implementations.
        /// </summary>
        public Type[] CommandHandlerImplementations { get; set; } = Array.Empty<Type>();

        /// <summary>
        /// Gets or sets the <see cref="ICommandHandler{TCommand}"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType CommandHandlerLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="IQuery{TResult}"/> implementations.
        /// </summary>
        public Type[] QueryImplementations { get; set; } = Array.Empty<Type>();

        /// <summary>
        /// Gets or sets the <see cref="IQuery{TResult}"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType QueryLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        /// <summary>
        /// Gets or sets the <see cref="IQueryHandler{TQuery, TResult}"/> implementations.
        /// </summary>
        public Type[] QueryHandlerImplementations { get; set; } = Array.Empty<Type>();

        /// <summary>
        /// Gets or sets the <see cref="IQueryHandler{TQuery, TResult}"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.PerScope"/>.</remarks>
        public LifetimeScopeType QueryHandlerLifetimeScope { get; set; } = LifetimeScopeType.PerScope;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="CqrsServiceRegistration"/>.
        /// </summary>
        public CqrsServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="CqrsServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public CqrsServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder.RegisterType(GetCommandDispatcherImplementation()).As<ICommandDispatcher>().SetLifetimeScope(CommandDispatcherLifetimeScope);
            Builder.RegisterType(GetQueryDispatcherImplementation()).As<IQueryDispatcher>().SetLifetimeScope(QueryDispatcherLifetimeScope);
            Builder.RegisterTypes(GetCommandHandlerImplementations()).AsClosedTypesOf(typeof(ICommandHandler<>)).SetLifetimeScope(CommandHandlerLifetimeScope);
            Builder.RegisterTypes(GetQueryImplementations()).AsClosedTypesOf(typeof(IQuery<>)).SetLifetimeScope(QueryHandlerLifetimeScope);
            Builder.RegisterTypes(GetQueryHandlerImplementations()).AsClosedTypesOf(typeof(IQueryHandler<,>)).SetLifetimeScope(QueryHandlerLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetCommandDispatcherImplementation() {
            if (CommandDispatcherImplementation != null) {
                return CommandDispatcherImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(ICommandDispatcher)).SingleOrDefault();
        }

        private Type GetQueryDispatcherImplementation() {
            if (QueryDispatcherImplementation != null) {
                return QueryDispatcherImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IQueryDispatcher)).SingleOrDefault();
        }

        private Type[] GetCommandHandlerImplementations() {
            if (!CommandHandlerImplementations.IsNullOrEmpty()) {
                return CommandHandlerImplementations;
            }

            return GetImplementationsFromSupportAssemblies(typeof(ICommandHandler<>));
        }

        private Type[] GetQueryImplementations() {
            if (!QueryImplementations.IsNullOrEmpty()) {
                return QueryImplementations;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IQuery<>));
        }

        private Type[] GetQueryHandlerImplementations() {
            if (!QueryHandlerImplementations.IsNullOrEmpty()) {
                return QueryHandlerImplementations;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IQueryHandler<,>));
        }

        #endregion Private Methods
    }
}