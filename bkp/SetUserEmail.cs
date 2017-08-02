using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserEmailCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string Email { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserEmailCommandHandler : CommandHandlerBase<SetUserEmailCommand> {

        #region Public Constructors

        public SetUserEmailCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserEmailCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserEmail,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.Email, command.Email)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}