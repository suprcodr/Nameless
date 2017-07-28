using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserLockoutEnabledQuery : IQuery<bool> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserLockoutEnabledQueryHandler : QueryHandlerBase<GetUserLockoutEnabledQuery, bool> {

        #region Public Constructors

        public GetUserLockoutEnabledQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<bool> HandleAsync(GetUserLockoutEnabledQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<bool>(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserLockoutEnabled,
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