using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Commands {

    public sealed class SetRoleNameCommand : ICommand {

        #region Public Properties

        public Guid RoleID { get; set; }
        public string Name { get; set; }

        #endregion Public Properties
    }

    public sealed class SetRoleNameCommandHandler : CommandHandlerBase<SetRoleNameCommand> {

        #region Public Constructors

        public SetRoleNameCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetRoleNameCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Roles.StoredProcedures.SetRoleName,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.ID, command.RoleID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.Name, command.Name)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}