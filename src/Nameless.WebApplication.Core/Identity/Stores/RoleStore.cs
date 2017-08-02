using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.CQRS.Query;
using Nameless.WebApplication.Core.Identity.Domains.Roles.Commands;
using Nameless.WebApplication.Core.Identity.Domains.Roles.Queries;
using Nameless.WebApplication.Core.Identity.Domains.RolesClaims.Commands;
using Nameless.WebApplication.Core.Identity.Domains.RolesClaims.Queries;
using Nameless.WebApplication.Core.Identity.Models;
using Resource = Nameless.WebApplication.Core.Properties.Resources;

namespace Nameless.WebApplication.Core.Identity.Stores {

    /// <summary>
    /// Default implementation for <see cref="IQueryableRoleStore{TRole}"/>,
    ///                            <see cref="IRoleClaimStore{TRole}"/>,
    ///                            <see cref="IRoleValidator{TRole}"/>,
    ///                            <see cref="IRoleStore{TRole}"/>.
    /// </summary>
    public class RoleStore :
        IQueryableRoleStore<Role>,
        IRoleClaimStore<Role>,
        IRoleValidator<Role>,
        IRoleStore<Role> {

        #region Private Read-Only Fields

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleStore(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) {
            Prevent.ParameterNull(commandDispatcher, nameof(commandDispatcher));
            Prevent.ParameterNull(queryDispatcher, nameof(queryDispatcher));

            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        #endregion Public Constructors

        #region Private Static Methods

        private static IdentityResult IdentityResultContinuation(Task continuation) {
            if (continuation.Exception is AggregateException ex) {
                return IdentityResult.Failed(new IdentityError {
                    Description = ex.InnerException.Message
                });
            }

            if (continuation.IsCanceled) {
                return IdentityResult.Failed(new IdentityError {
                    Description = Resource.TaskCanceled
                });
            }

            return IdentityResult.Success;
        }

        #endregion Private Static Methods

        #region IQueryableRoleStore<Role> Members

        /// <inheritdoc />
        public IQueryable<Role> Roles {
            get { throw new NotImplementedException(); }
        }

        #endregion IQueryableRoleStore<Role> Members

        #region IRoleClaimStore<Role> Members

        /// <inheritdoc />
        public Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default(CancellationToken)) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(claim, nameof(claim));

            return _commandDispatcher.CommandAsync(new AddClaimToRoleCommand {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                RoleID = role.ID,
            }, cancellationToken: cancellationToken);
        }

        #endregion IRoleClaimStore<Role> Members

        #region IRoleValidator<Role> Members

        /// <inheritdoc />
        public async Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role) {
            Prevent.ParameterNull(manager, nameof(manager));
            Prevent.ParameterNull(role, nameof(role));

            var errors = new List<IdentityError>();
            await ValidateRoleNameAsync(manager, role, errors);

            return errors.Count > 0
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }

        private async Task ValidateRoleNameAsync(RoleManager<Role> manager, Role role, ICollection<IdentityError> errors) {
            var roleName = await manager.GetRoleNameAsync(role);
            if (!string.IsNullOrWhiteSpace(roleName)) {
                var owner = await manager.FindByNameAsync(roleName);
                if (owner != null && !string.Equals(await manager.GetRoleIdAsync(owner), await manager.GetRoleIdAsync(role))) {
                    errors.Add(new IdentityError { Description = Resource.Validation_DuplicateRoleName });
                }
            } else { errors.Add(new IdentityError { Description = Resource.Validation_InvalidRoleName }); }
        }

        #endregion IRoleValidator<Role> Members

        #region IRoleStore<Role> Members

        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return _commandDispatcher.CommandAsync(new CreateRoleCommand {
                Name = role.Name,
            }, cancellationToken: cancellationToken)
            .ContinueWith(IdentityResultContinuation);
        }

        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return _commandDispatcher.CommandAsync(new DeleteRoleCommand {
                RoleID = role.ID
            }, cancellationToken: cancellationToken)
            .ContinueWith(IdentityResultContinuation);
        }

        /// <inheritdoc />
        public void Dispose() {
        }

        /// <inheritdoc />
        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(roleId, nameof(roleId));

            return _queryDispatcher.QueryAsync(new FindRoleByIDQuery {
                RoleID = roleId
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(normalizedRoleName, nameof(normalizedRoleName));

            return _queryDispatcher.QueryAsync(new FindRoleByNormalizedNameQuery {
                NormalizedName = normalizedRoleName
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken = default(CancellationToken)) {
            Prevent.ParameterNull(role, nameof(role));

            return _queryDispatcher.QueryAsync(new GetRoleClaimsQuery {
                RoleID = role.ID
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.NormalizedName);
        }

        /// <inheritdoc />
        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.ID.ToString());
        }

        /// <inheritdoc />
        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(role.Name);
        }

        /// <inheritdoc />
        public Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default(CancellationToken)) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(claim, nameof(claim));

            return _commandDispatcher.CommandAsync(new RemoveRoleClaimCommand {
                RoleID = role.ID,
                ClaimType = claim.Type
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(normalizedName, nameof(normalizedName));

            cancellationToken.ThrowIfCancellationRequested();

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(roleName, nameof(roleName));

            cancellationToken.ThrowIfCancellationRequested();

            role.Name = roleName;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return _commandDispatcher.CommandAsync(new UpdateRoleCommand {
                RoleID = role.ID,
                Name = role.Name,
                NormalizedName = role.NormalizedName
            }).ContinueWith(IdentityResultContinuation);
        }

        #endregion IRoleStore<Role> Members
    }
}