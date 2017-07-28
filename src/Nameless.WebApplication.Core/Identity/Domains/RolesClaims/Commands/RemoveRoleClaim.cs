using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.RolesClaims.Commands {

    public sealed class RemoveRoleClaimCommand : ICommand {

        #region Public Properties

        public Guid RoleID { get; set; }
        public string ClaimType { get; set; }

        #endregion Public Properties
    }

    public sealed class RemoveRoleClaimCommandHandler : CommandHandlerBase<RemoveRoleClaimCommand> {

        #region Public Constructors

        public RemoveRoleClaimCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(RemoveRoleClaimCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.RolesClaims.StoredProcedures.RemoveRoleClaim,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.RolesClaims.Fields.RoleID, command.RoleID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.RolesClaims.Fields.Type, command.ClaimType)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}