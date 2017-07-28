using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.UsersLogins.Commands {

    public sealed class RemoveUserLoginCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string LoginProvider { get; set; }

        #endregion Public Properties
    }

    public sealed class RemoveUserLoginCommandHandler : CommandHandlerBase<RemoveUserLoginCommand> {

        #region Public Constructors

        public RemoveUserLoginCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(RemoveUserLoginCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.UsersLogins.StoredProcedures.RemoveUserLogin,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.UserID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.LoginProvider, command.LoginProvider)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}