using Nameless.Skeleton.Framework.IoC;

namespace Nameless.Skeleton.Framework.Data.Sql.Ado {

    public class ActionInformationExtractorFactory : IActionInformationExtractorFactory {

        #region Private Read-Only Fields

        private readonly IResolver _resolver;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public ActionInformationExtractorFactory(IResolver resolver) {
            Prevent.ParameterNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        #endregion Public Constructors

        #region IActionInformationExtractorFactory Members

        public IActionInformationExtractor<TEntity> Create<TEntity>() where TEntity : class {
            return _resolver.Resolve<IActionInformationExtractor<TEntity>>();
        }

        #endregion IActionInformationExtractorFactory Members
    }
}