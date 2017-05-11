using System.Data;
using Nameless.Skeleton.Framework.Cqrs.Command;
using Nameless.Skeleton.Framework.Data.Sql.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Resources;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Commands {

    public class UserLoginCommandHandler : ICommandHandler<AddLoginToUserCommand>,
                                           ICommandHandler<RemoveLoginFromUserCommand> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserLoginCommandHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region ICommandHandler<AddLoginToUserCommand> Members

        public void Handle(AddLoginToUserCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.AddLoginToUser, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.LoginProvider), message.LoginProvider),
                Parameter.CreateInputParameter(nameof(message.ProviderKey), message.ProviderKey)
            });
        }

        #endregion ICommandHandler<AddLoginToUserCommand> Members

        #region ICommandHandler<RemoveLoginFromUserCommand> Members

        public void Handle(RemoveLoginFromUserCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.RemoveLoginFromUser, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.LoginProvider), message.LoginProvider)
            });
        }

        #endregion ICommandHandler<RemoveLoginFromUserCommand> Members
    }
}