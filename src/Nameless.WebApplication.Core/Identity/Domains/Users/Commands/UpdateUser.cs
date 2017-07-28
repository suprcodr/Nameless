using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class UpdateUserCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public byte[] ProfilePictureBlob { get; set; }

        #endregion Public Properties
    }

    public sealed class UpdateUserCommandHandler : CommandHandlerBase<UpdateUserCommand> {

        #region Public Constructors

        public UpdateUserCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(UpdateUserCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.UpdateUser,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.FullName, command.FullName),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ProfilePicture, command.ProfilePicture),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ProfilePictureBlob, command.ProfilePictureBlob, DbType.Binary)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}