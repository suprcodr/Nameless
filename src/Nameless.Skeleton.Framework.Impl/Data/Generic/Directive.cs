using Microsoft.EntityFrameworkCore;

namespace Nameless.Skeleton.Framework.Data.Generic {

    /// <summary>
    /// Represents a directive to Entity Framework <see cref="DbContext"/>.
    /// Useful when need to execute a procedure.
    /// </summary>
    public abstract class Directive : IDirective {

        #region Protected Properties

        protected DbContext DbContext { get; }

        #endregion Protected Properties

        #region Protected Constructors

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="dbContext">The Entity Framework database context.</param>
        protected Directive(DbContext dbContext) {
            Prevent.ParameterNull(dbContext, nameof(dbContext));

            DbContext = dbContext;
        }

        #endregion Protected Constructors

        #region IDirective Members

        public abstract dynamic Execute(dynamic parameters);

        #endregion IDirective Members
    }
}