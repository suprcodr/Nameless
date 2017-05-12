using Nameless.Skeleton.Framework.IoC;

namespace Nameless.Skeleton.Framework.Cqrs.Query {

    public class QueryDispatcher : IQueryDispatcher {

        #region Private Read-Only Fields

        private readonly IResolver _resolver;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public QueryDispatcher(IResolver resolver) {
            Prevent.ParameterNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        #endregion Public Constructors

        #region IQueryDispatcher Members

        public TResult Query<TResult>(IQuery<TResult> query) {
            Prevent.ParameterNull(query, nameof(query));

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _resolver.Resolve(handlerType);

            return handler.Handle((dynamic)query);
        }

        #endregion IQueryDispatcher Members
    }
}