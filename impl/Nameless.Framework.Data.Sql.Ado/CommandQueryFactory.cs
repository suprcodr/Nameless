using Nameless.Framework.IoC;

namespace Nameless.Framework.Data.Sql.Ado {

    public class CommandQueryFactory : ICommandQueryFactory {

        #region Private Read-Only Fields

        private readonly IResolver _resolver;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public CommandQueryFactory(IResolver resolver) {
            Prevent.ParameterNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        #endregion Public Constructors

        #region IActionInformationExtractorFactory Members

        public ICommandQuery<TEntity> Create<TEntity>() where TEntity : class {
            return _resolver.Resolve<ICommandQuery<TEntity>>();
        }

        #endregion IActionInformationExtractorFactory Members
    }
}