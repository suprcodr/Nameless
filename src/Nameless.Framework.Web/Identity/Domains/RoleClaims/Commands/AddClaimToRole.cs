using System;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Data;
using Nameless.Framework.Web.Identity.Data.RoleClaims;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class AddClaimToRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public RoleClaim Claim { get; set; }

        #endregion Public Properties
    }

    public class AddClaimToRoleCommandHandler : ICommandHandler<AddClaimToRoleCommand> {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public AddClaimToRoleCommandHandler(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region ICommandHandler<AddClaimsToRoleCommand> Members

        public Task HandleAsync(AddClaimToRoleCommand message, IProgress<int> progress = null, CancellationToken cancellationToken = default(CancellationToken)) {
            return _repository.SaveAsync(new AddClaimToRole {
            }, cancellationToken);
        }

        #endregion ICommandHandler<AddClaimsToRoleCommand> Members
    }
}