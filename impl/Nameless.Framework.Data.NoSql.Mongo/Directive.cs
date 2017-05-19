using MongoDB.Driver;

namespace Nameless.Framework.Data.NoSql.Mongo {

    public abstract class Directive : IDirective {

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

        public abstract dynamic Execute(dynamic parameters);

        #endregion IDirective Members
    }
}