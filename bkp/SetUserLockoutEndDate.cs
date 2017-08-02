using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserLockoutEndDateCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public DateTimeOffset? LockoutEndDateUtc { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserLockoutEndDateCommandHandler : CommandHandlerBase<SetUserLockoutEndDateCommand> {

        #region Public Constructors

        public SetUserLockoutEndDateCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserLockoutEndDateCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserLockoutEndDate,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.LockoutEndDateUtc, command.LockoutEndDateUtc, DbType.DateTimeOffset)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}