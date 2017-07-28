using System;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Generic;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class AddClaimToRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleClaimID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Guid RoleID { get; set; }

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

        public Task HandleAsync(AddClaimToRoleCommand message, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return _repository.SaveAsync(cancellationToken, progress, new RoleClaim(message.RoleClaimID) {
                Type = message.Type,
                Value = message.Value,
                Role = new Role(message.RoleID)
            });
        }

        #endregion ICommandHandler<AddClaimsToRoleCommand> Members
    }
}