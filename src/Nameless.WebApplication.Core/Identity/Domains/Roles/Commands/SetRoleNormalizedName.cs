using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Commands {

    public sealed class SetRoleNormalizedNameCommand : ICommand {

        #region Public Properties

        public Guid RoleID { get; set; }
        public string NormalizedName { get; set; }

        #endregion Public Properties
    }

    public sealed class SetRoleNormalizedNameCommandHandler : CommandHandlerBase<SetRoleNormalizedNameCommand> {

        #region Public Constructors

        public SetRoleNormalizedNameCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetRoleNormalizedNameCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Roles.StoredProcedures.SetRoleNormalizedName,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.ID, command.RoleID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.NormalizedName, command.NormalizedName, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}