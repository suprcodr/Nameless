using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.UsersLogins.Commands {

    public sealed class AddUserLoginCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string DisplayName { get; set; }

        #endregion Public Properties
    }

    public sealed class AddUserLoginCommandHandler : CommandHandlerBase<AddUserLoginCommand> {

        #region Public Constructors

        public AddUserLoginCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(AddUserLoginCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.UsersLogins.StoredProcedures.AddUserLogin,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.UserID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.LoginProvider, command.LoginProvider),
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.ProviderKey, command.ProviderKey),
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.DisplayName, command.DisplayName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}