using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Commands {

    public sealed class UpdateRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleID { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        #endregion Public Properties
    }

    public sealed class UpdateRoleCommandHandler : CommandHandlerBase<UpdateRoleCommand> {

        #region Public Constructors

        public UpdateRoleCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(UpdateRoleCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Roles.StoredProcedures.UpdateRole,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.ID, command.RoleID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.Name, command.Name),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.NormalizedName, command.NormalizedName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}