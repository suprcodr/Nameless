using System.Data;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Data.Sql.Ado;
using Nameless.Framework.Web.Identity.Domains.Resources;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Commands {

    public class RoleCommandHandler : ICommandHandler<CreateRoleCommand>,
                                      ICommandHandler<DeleteRoleCommand>,
                                      ICommandHandler<RemoveRoleClaimCommand>,
                                      ICommandHandler<SetRoleNormalizedRoleNameCommand>,
                                      ICommandHandler<SetRoleRoleNameCommand>,
                                      ICommandHandler<UpdateRoleCommand> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleCommandHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region ICommandHandler<CreateRoleCommand> Members

        public void Handle(CreateRoleCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.CreateRole, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.ConcurrencyStamp), message.ConcurrencyStamp),
                Parameter.CreateInputParameter(nameof(message.Name), message.Name),
                Parameter.CreateInputParameter(nameof(message.NormalizedName), message.NormalizedName)
            });
        }

        #endregion ICommandHandler<CreateRoleCommand> Members

        #region ICommandHandler<DeleteRoleCommand> Members

        public void Handle(DeleteRoleCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.CreateRole, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid)
            });
        }

        #endregion ICommandHandler<DeleteRoleCommand> Members

        #region ICommandHandler<RemoveRoleClaimCommand> Members

        public void Handle(RemoveRoleClaimCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.RemoveRoleClaim, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Type), message.Type),
                Parameter.CreateInputParameter(nameof(message.Value), message.Value)
            });
        }

        #endregion ICommandHandler<RemoveRoleClaimCommand> Members

        #region ICommandHandler<SetRoleNormalizedRoleNameCommand>

        public void Handle(SetRoleNormalizedRoleNameCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.CreateRole, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.NormalizedName), message.NormalizedName)
            });
        }

        #endregion ICommandHandler<SetRoleNormalizedRoleNameCommand>

        #region ICommandHandler<SetRoleRoleNameCommand> Members

        public void Handle(SetRoleRoleNameCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.CreateRole, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Name), message.Name)
            });
        }

        #endregion ICommandHandler<SetRoleRoleNameCommand> Members

        #region ICommandHandler<UpdateRoleCommand> Members

        public void Handle(UpdateRoleCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.CreateRole, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.ConcurrencyStamp), message.ConcurrencyStamp),
                Parameter.CreateInputParameter(nameof(message.Name), message.Name),
                Parameter.CreateInputParameter(nameof(message.NormalizedName), message.NormalizedName)
            });
        }

        #endregion ICommandHandler<UpdateRoleCommand> Members
    }
}