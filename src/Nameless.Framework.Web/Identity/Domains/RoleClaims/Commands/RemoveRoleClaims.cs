using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Data;
using Nameless.Framework.Web.Identity.Data.RoleClaims;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class RemoveRoleClaimsCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public IEnumerable<RoleClaim> Claims { get; set; }

        #endregion Public Properties
    }

    public class RemoveRoleClaimsCommandHandler : ICommandHandler<RemoveRoleClaimsCommand> {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RemoveRoleClaimsCommandHandler(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region ICommandHandler<RemoveRoleClaimsCommand> Members

        public Task HandleAsync(RemoveRoleClaimsCommand message, IProgress<int> progress = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                message.Claims.Each(claim => {
                    cancellationToken.ThrowIfCancellationRequested();

                    _repository.Delete(new RemoveRoleClaim {
                        RoleID = message.RoleId,
                        Type = claim.Type,
                        Value = claim.Value
                    });
                });
            }, cancellationToken);
        }

        #endregion ICommandHandler<RemoveRoleClaimsCommand> Members
    }
}