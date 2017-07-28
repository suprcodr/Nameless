using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.RolesClaims.Commands {

    public class AddClaimToRoleCommand : ICommand {

        #region Public Properties

        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public Guid RoleID { get; set; }

        #endregion Public Properties
    }

    public class AddClaimToRoleCommandHandler : CommandHandlerBase<AddClaimToRoleCommand> {

        #region Public Constructors

        public AddClaimToRoleCommandHandler(IDatabase database)
            : base(database) {
        }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(AddClaimToRoleCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.RolesClaims.StoredProcedures.AddClaimToRole,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.RolesClaims.Fields.RoleID, command.RoleID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.RolesClaims.Fields.Type, command.ClaimType, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.RolesClaims.Fields.Value, command.ClaimValue, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}