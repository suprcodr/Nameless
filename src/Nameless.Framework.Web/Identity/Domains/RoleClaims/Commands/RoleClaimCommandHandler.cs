using System;
using System.Data;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Data.Sql.Ado;
using Nameless.Framework.Web.Identity.Domains.Resources;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class RoleClaimCommandHandler : ICommandHandler<AddClaimToRoleCommand>,
                                           ICommandHandler<RemoveRoleClaimsCommand>,
                                           ICommandHandler<ReplaceRoleClaimCommand> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleClaimCommandHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region ICommandHandler<AddClaimsToRoleCommand> Members

        public void Handle(AddClaimToRoleCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.AddClaimToRole, parameters: new[] {
                    Parameter.CreateInputParameter("RoleClaimId", Guid.NewGuid(), DbType.Guid),
                    Parameter.CreateInputParameter(nameof(message.Claim.Type), message.Claim.Type),
                    Parameter.CreateInputParameter(nameof(message.Claim.Value), message.Claim.Value),
                    Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid)
                });
        }

        #endregion ICommandHandler<AddClaimsToRoleCommand> Members

        #region ICommandHandler<RemoveRoleClaimsCommand> Members

        public void Handle(RemoveRoleClaimsCommand message) {
            message.Claims.Each(claim => {
                _database.ExecuteNonQuery(SQL.Instance.RemoveRoleClaims, parameters: new[] {
                    Parameter.CreateInputParameter(nameof(claim.Type), claim.Type),
                    Parameter.CreateInputParameter(nameof(claim.Value), claim.Value),
                    Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid)
                });
            });
        }

        #endregion ICommandHandler<RemoveRoleClaimsCommand> Members

        #region ICommandHandler<ReplaceRoleClaimCommand> Members

        public void Handle(ReplaceRoleClaimCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.ReplaceRoleClaim, parameters: new[] {
                    Parameter.CreateInputParameter("NewType", message.NewClaim.Type),
                    Parameter.CreateInputParameter("NewValue", message.NewClaim.Value),
                    Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid),
                    Parameter.CreateInputParameter("OldType", message.OldClaim.Type),
                    Parameter.CreateInputParameter("OldValue", message.OldClaim.Value)
                });
        }

        #endregion ICommandHandler<ReplaceRoleClaimCommand> Members
    }
}