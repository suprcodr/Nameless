using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class RemoveUserFromRoleCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string RoleName { get; set; }

        #endregion Public Properties
    }

    public sealed class RemoveUserFromRoleCommandHandler : CommandHandlerBase<RemoveUserFromRoleCommand> {

        #region Public Constructors

        public RemoveUserFromRoleCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(RemoveUserFromRoleCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.RemoveUserFromRole,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.Name, command.RoleName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}