using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Commands {

    public sealed class CreateRoleCommand : ICommand {

        #region Public Properties

        public string Name { get; set; }

        #endregion Public Properties
    }

    public sealed class CreateRoleCommandHandler : CommandHandlerBase<CreateRoleCommand> {

        #region Public Constructors

        public CreateRoleCommandHandler(IApplicationContext appContext, IDatabase database)
            : base(appContext, database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(CreateRoleCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Roles.StoredProcedures.CreateRole,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.ID, Guid.NewGuid(), DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.Name, command.Name),
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.OwnerID, AppContext.Owner.ID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}