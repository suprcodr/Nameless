using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Nameless.Framework.Data.Generic.NoSql.Mongo {

    public abstract class Directive<TResult> : IDirective<TResult> {

        #region Protected Properties

        protected IMongoDatabase Database { get; }

        #endregion Protected Properties

        #region Protected Constructors

        protected Directive(IMongoDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            Database = database;
        }

        #endregion Protected Constructors

        #region IDirective Members

        public abstract Task<TResult> ExecuteAsync(NameValueParameterSet parameters, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null);

        #endregion IDirective Members
    }
}