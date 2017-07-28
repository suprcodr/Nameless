using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Nameless.Framework.IoC;

namespace Nameless.Framework.Data.Ado.IoC {

    /// <summary>
    /// Service registration for ADO data access.
    /// </summary>
    public class AdoServiceRegistration : ServiceRegistrationBase {

        #region Public Properties

        /// <summary>
        /// Should use transaction.
        /// </summary>
        public bool UseTransaction { get; set; }

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
            Builder
                .RegisterType<DbConnectionProvider>()
                .Named<IDbConnectionProvider>(typeof(DbConnectionProvider).FullName)
                .SetLifetimeScope(LifetimeScopeType.PerScope);

            var databaseRegistration = Builder
                .RegisterType<Database>()
                .WithParameters(new[] {
                    new ResolvedParameter(
                        (param, ctx) => param.ParameterType == typeof(DatabaseSettings),
                        (param, ctx) => ctx.Resolve<DatabaseSettings>()
                    ),
                    new ResolvedParameter(
                        (param, ctx) => param.ParameterType == typeof(IDbConnectionProvider),
                        (param, ctx) => ctx.ResolveNamed<IDbConnectionProvider>(typeof(DbConnectionProvider).FullName)
                    )
                });
            if (UseTransaction) {
                databaseRegistration
                    .Named<IDatabase>(typeof(Database).FullName);

                Builder
                    .RegisterDecorator<IDatabase>(
                        (ctx, database) => new TransactionAwareDatabase(database),
                        fromKey: typeof(Database).FullName
                    )
                    .SetLifetimeScope(LifetimeScopeType.PerScope);
            } else { databaseRegistration.As<IDatabase>(); }
            databaseRegistration.SetLifetimeScope(LifetimeScopeType.PerScope);
        }

        #endregion Public Override Methods
    }
}