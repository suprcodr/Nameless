using System;
using System.Data;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Data.Sql.Ado;
using Nameless.Framework.Web.Identity.Domains.Resources;

namespace Nameless.Framework.Web.Identity.Domains.UserClaims.Commands {

    public class UserClaimCommandHandler : ICommandHandler<AddClaimsToUserCommand>,
                                           ICommandHandler<RemoveUserClaimsCommand>,
                                           ICommandHandler<ReplaceUserClaimCommand> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserClaimCommandHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region ICommandHandler<AddClaimsToUserCommand> Members

        public void Handle(AddClaimsToUserCommand message) {
            message.Claims.Each(claim => {
                _database.ExecuteNonQuery(SQL.Instance.AddClaimsToUser, parameters: new[] {
                    Parameter.CreateInputParameter("UserClaimId", Guid.NewGuid(), DbType.Guid),
                    Parameter.CreateInputParameter(nameof(claim.Type), claim.Type),
                    Parameter.CreateInputParameter(nameof(claim.Value), claim.Value),
                    Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid)
                });
            });
        }

        #endregion ICommandHandler<AddClaimsToUserCommand> Members

        #region ICommandHandler<RemoveUserClaimsCommand> Members

        public void Handle(RemoveUserClaimsCommand message) {
            message.Claims.Each(claim => {
                _database.ExecuteNonQuery(SQL.Instance.RemoveUserClaims, parameters: new[] {
                    Parameter.CreateInputParameter(nameof(claim.Type), claim.Type),
                    Parameter.CreateInputParameter(nameof(claim.Value), claim.Value),
                    Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid)
                });
            });
        }

        #endregion ICommandHandler<RemoveUserClaimsCommand> Members

        #region ICommandHandler<ReplaceUserClaimCommand> Members

        public void Handle(ReplaceUserClaimCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.ReplaceUserClaim, parameters: new[] {
                    Parameter.CreateInputParameter("NewType", message.NewClaim.Type),
                    Parameter.CreateInputParameter("NewValue", message.NewClaim.Value),
                    Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                    Parameter.CreateInputParameter("OldType", message.OldClaim.Type),
                    Parameter.CreateInputParameter("OldValue", message.OldClaim.Value)
                });
        }

        #endregion ICommandHandler<ReplaceUserClaimCommand> Members
    }
}