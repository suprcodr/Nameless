using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class CreateUserCommand : ICommand {

        #region Public Properties

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public byte[] ProfilePictureBlob { get; set; }
        public Guid OwnerID { get; set; }

        #endregion Public Properties
    }

    public sealed class CreateUserCommandHandler : CommandHandlerBase<CreateUserCommand> {

        #region Public Constructors

        public CreateUserCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.CreateUser,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, Guid.NewGuid(), DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.UserName, command.UserName),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.Email, command.Email),
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