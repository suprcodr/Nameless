using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class AddUserToRoleCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string RoleName { get; set; }

        #endregion Public Properties
    }

    public sealed class AddUserToRoleCommandHandler : CommandHandlerBase<AddUserToRoleCommand> {

        #region Public Constructors

        public AddUserToRoleCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(AddUserToRoleCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.AddUserToRole,
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