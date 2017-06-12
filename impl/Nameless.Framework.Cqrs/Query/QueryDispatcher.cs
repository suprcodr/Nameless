using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.IoC;

namespace Nameless.Framework.Cqrs.Query {

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

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken) {
            Prevent.ParameterNull(query, nameof(query));

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _resolver.Resolve(handlerType);

            return handler.HandleAsync((dynamic)query, cancellationToken);
        }

        #endregion IQueryDispatcher Members
    }
}