using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserPasswordHashCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string PasswordHash { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserPasswordHashCommandHandler : CommandHandlerBase<SetUserPasswordHashCommand> {

        #region Public Constructors

        public SetUserPasswordHashCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserPasswordHashCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserPasswordHash,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.PasswordHash, command.PasswordHash)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}