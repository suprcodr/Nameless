using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserNameCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string UserName { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserNameCommandHandler : CommandHandlerBase<SetUserNameCommand> {

        #region Public Constructors

        public SetUserNameCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserNameCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserName,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.UserName, command.UserName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}