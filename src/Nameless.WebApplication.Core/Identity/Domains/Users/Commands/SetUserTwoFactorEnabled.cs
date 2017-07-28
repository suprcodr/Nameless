using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserTwoFactorEnabledCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public bool Enabled { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserTwoFactorEnabledCommandHandler : CommandHandlerBase<SetUserTwoFactorEnabledCommand> {

        #region Public Constructors

        public SetUserTwoFactorEnabledCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserTwoFactorEnabledCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserTwoFactorEnabled,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.TwoFactorEnabled, command.Enabled, DbType.Boolean)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}