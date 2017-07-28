using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserLockoutEnabledCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public bool LockoutEnabled { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserLockoutEnabledCommandHandler : CommandHandlerBase<SetUserLockoutEnabledCommand> {

        #region Public Constructors

        public SetUserLockoutEnabledCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserLockoutEnabledCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserLockoutEnabled,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.LockoutEnabled, command.LockoutEnabled)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}