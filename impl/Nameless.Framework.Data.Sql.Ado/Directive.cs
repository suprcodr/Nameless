using System.Data;

namespace Nameless.Framework.Data.Sql.Ado {

    /// <summary>
    /// Represents a directive to ADO.
    /// Useful when need to execute a procedure.
    /// </summary>
    public abstract class Directive : IDirective {

        #region Protected Properties

        protected IDbConnection Connection { get; }

        #endregion Protected Properties

        #region Protected Constructors

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="connection">The ADO database connection.</param>
        protected Directive(IDbConnection connection) {
            Prevent.ParameterNull(connection, nameof(connection));

            Connection = connection;
        }

        #endregion Protected Constructors

        #region IDirective Members

        public abstract dynamic Execute(dynamic parameters);

        #endregion IDirective Members
    }
}