using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserLockoutEndDateQuery : IQuery<DateTimeOffset?> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserLockoutEndDateQueryHandler : QueryHandlerBase<GetUserLockoutEndDateQuery, DateTimeOffset?> {

        #region Public Constructors

        public GetUserLockoutEndDateQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<DateTimeOffset?> HandleAsync(GetUserLockoutEndDateQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<DateTimeOffset?>(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserLockoutEndDate,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, query.UserID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}