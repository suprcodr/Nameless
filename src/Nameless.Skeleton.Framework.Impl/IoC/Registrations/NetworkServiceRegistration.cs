﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Nameless.Skeleton.Framework.Network.PubSub;

namespace Nameless.Skeleton.Framework.IoC.Modules {

    /// <summary>
    /// Autofac module implementation for Nameless.Skeleton.Framework.Network namespace.
    /// </summary>
    public class NetworkServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="IPublisherSubscriber"/> implementation.
        /// </summary>
        /// <remarks>Default is <see cref="PublisherSubscriber"/>.</remarks>
        public Type PublisherSubscriberImplementation { get; set; } = typeof(PublisherSubscriber);

        /// <summary>
        /// Gets or sets the <see cref="IPublisherSubscriber"/> <see cref="LifetimeScopeType"/>.
        /// </summary>
        /// <remarks>Default is <see cref="LifetimeScopeType.Singleton"/>.</remarks>
        public LifetimeScopeType PublisherSubscriberLifetimeScope { get; set; } = LifetimeScopeType.Singleton;

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="NetworkServiceRegistration"/>.
        /// </summary>
        public NetworkServiceRegistration()
            : base(null) { }

        /// <summary>
        /// Initializes a new instance of <see cref="NetworkServiceRegistration"/>.
        /// </summary>
        /// <param name="supportAssemblies">The support assemblies.</param>
        public NetworkServiceRegistration(IEnumerable<Assembly> supportAssemblies)
            : base(supportAssemblies) { }

        #endregion Public Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override void Register() {
            Builder.RegisterType(GetPublisherSubscriberImplementation()).As<IPublisherSubscriber>().SetLifetimeScope(PublisherSubscriberLifetimeScope);
        }

        #endregion Public Override Methods

        #region Private Methods

        private Type GetPublisherSubscriberImplementation() {
            if (PublisherSubscriberImplementation != null) {
                return PublisherSubscriberImplementation;
            }

            return GetImplementationsFromSupportAssemblies(typeof(IPublisherSubscriber)).SingleOrDefault();
        }

        #endregion Private Methods
    }
}