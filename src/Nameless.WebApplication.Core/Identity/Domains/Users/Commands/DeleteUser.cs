using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class DeleteUserCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class DeleteUserCommandHandler : CommandHandlerBase<DeleteUserCommand> {

        #region Public Constructors

        public DeleteUserCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.DeleteUser,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}