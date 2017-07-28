using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Queries {

    public sealed class GetRoleNameQuery : IQuery<string> {

        #region Public Properties

        public Guid RoleID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetRoleNameQueryHandler : QueryHandlerBase<GetRoleNameQuery, string> {

        #region Public Constructors

        public GetRoleNameQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<string> HandleAsync(GetRoleNameQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<string>(
                    commandText: EntitySchema.Roles.StoredProcedures.GetRoleName,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.ID, query.RoleID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}