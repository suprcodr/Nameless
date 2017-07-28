using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Nameless.Framework.Data.Generic.NoSql.Mongo {

    public sealed class DirectiveExecutor : IDirectiveExecutor {

        #region Private Read-Only Fields

        private readonly IMongoDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public DirectiveExecutor(IMongoDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region Private Methods

        private IMongoCollection<TEntity> GetCollection<TEntity>() {
            return _database.GetCollection<TEntity>(typeof(TEntity).FullName);
        }

        #endregion Private Methods

        #region IDirectiveExecutor Members

        public Task<TResult> ExecuteDirectiveAsync<TResult, TDirective>(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) where TDirective : IDirective<TResult> {
            if (!typeof(Directive<>).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive<>)}\"");
            }

            var directive = (IDirective<TResult>)Activator.CreateInstance(typeof(TDirective), new object[] { _database });

            return directive.ExecuteAsync(parameters, cancellationToken, progress ?? NullProgress<int>.Instance);
        }

        #endregion IDirectiveExecutor Members
    }
}