using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Generic;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.Web.Identity.Domains.Resources;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class RoleClaimCommandHandler : ICommandHandler<AddClaimToRoleCommand>,
                                           ICommandHandler<RemoveRoleClaimsCommand>,
                                           ICommandHandler<ReplaceRoleClaimCommand> {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleClaimCommandHandler(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region ICommandHandler<AddClaimsToRoleCommand> Members

        public Task HandleAsync(AddClaimToRoleCommand message, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return _repository.Save(new AddClaimToRole)

            _repository.ExecuteNonQuery(SQL.Instance.AddClaimToRole, parameters: new[] {
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
                _repository.ExecuteNonQuery(SQL.Instance.RemoveRoleClaims, parameters: new[] {
                    Parameter.CreateInputParameter(nameof(claim.Type), claim.Type),
                    Parameter.CreateInputParameter(nameof(claim.Value), claim.Value),
                    Parameter.CreateInputParameter(nameof(message.RoleId), message.RoleId, DbType.Guid)
                });
            });
        }

        #endregion ICommandHandler<RemoveRoleClaimsCommand> Members

        #region ICommandHandler<ReplaceRoleClaimCommand> Members

        public void Handle(ReplaceRoleClaimCommand message) {
            _repository.ExecuteNonQuery(SQL.Instance.ReplaceRoleClaim, parameters: new[] {
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