using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserEmailConfirmedCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public bool Confirmed { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserEmailConfirmedCommandHandler : CommandHandlerBase<SetUserEmailConfirmedCommand> {

        #region Public Constructors

        public SetUserEmailConfirmedCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserEmailConfirmedCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserEmailConfirmed,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.EmailConfirmed, command.Confirmed, DbType.Boolean)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}