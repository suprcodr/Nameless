using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Cqrs.Query;
using Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands;
using Nameless.Framework.Web.Identity.Domains.Roles.Commands;
using Nameless.Framework.Web.Identity.Domains.Roles.Queries;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Stores {

    /// <summary>
    /// Default implementation for <see cref="IQueryableRoleStore{TRole}"/>, <see cref="IRoleClaimStore{TRole}"/>, <see cref="IRoleValidator{TRole}"/>, <see cref="IRoleStore{TRole}"/>.
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

            return Task.Run(() => {
                _commandDispatcher.Command(new AddClaimToRoleCommand {
                    RoleId = role.Id,
                    Claim = claim.ConvertToRoleClaim()
                });
            }, cancellationToken);
        }

        #endregion IRoleClaimStore<Role> Members

        #region IRoleValidator<Role> Members

        /// <inheritdoc />
        public async Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role) {
            Prevent.ParameterNull(manager, nameof(manager));
            Prevent.ParameterNull(role, nameof(role));

            var errors = new List<IdentityError>();
            await ValidateRoleName(manager, role, errors);

            return errors.Count > 0
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }

        private async Task ValidateRoleName(RoleManager<Role> manager, Role role, ICollection<IdentityError> errors) {
            var roleName = await manager.GetRoleNameAsync(role);
            if (!string.IsNullOrWhiteSpace(roleName)) {
                var owner = await manager.FindByNameAsync(roleName);
                if (owner != null && !string.Equals(await manager.GetRoleIdAsync(owner), await manager.GetRoleIdAsync(role))) {
                    errors.Add(new IdentityError { Description = "Duplicated role name." });
                }
            } else {
                errors.Add(new IdentityError { Description = "Invalid role name." });
            }
        }

        #endregion IRoleValidator<Role> Members

        #region IRoleStore<Role> Members

        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run(() => {
                try {
                    _commandDispatcher.Command(new CreateRoleCommand {
                        RoleId = Guid.NewGuid(),
                        ConcurrencyStamp = string.Empty,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName
                    });
                } catch (Exception ex) {
                    return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                }

                return IdentityResult.Success;
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run(() => {
                try {
                    _commandDispatcher.Command(new DeleteRoleCommand {
                        RoleId = role.Id
                    });
                } catch (Exception ex) {
                    return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                }

                return IdentityResult.Success;
            }, cancellationToken);
        }

        /// <inheritdoc />
        public void Dispose() {
        }

        /// <inheritdoc />
        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken) {
            Prevent.ParameterNull(roleId, nameof(roleId));

            return Task.Run(() => {
                return _queryDispatcher.Query(new FindRoleByIdQuery { RoleId = Guid.Parse(roleId) });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(normalizedRoleName, nameof(normalizedRoleName));

            return Task.Run(() => {
                return _queryDispatcher.Query(new FindRoleByNameQuery { NormalizedName = normalizedRoleName });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken = default(CancellationToken)) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run<IList<Claim>>(() => {
                return _queryDispatcher.Query(new GetRoleClaimsQuery { RoleId = role.Id })
                        .Select(_ => _.ConvertFromRoleClaim())
                        .ToList();
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetRoleNormalizedRoleNameQuery { RoleId = role.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetRoleRoleIdQuery { NormalizedName = role.NormalizedName }).ToString();
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetRoleRoleNameQuery { RoleId = role.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default(CancellationToken)) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(claim, nameof(claim));

            return Task.Run(() => {
                _commandDispatcher.Command(new RemoveRoleClaimCommand {
                    RoleId = role.Id,
                    Type = claim.Type,
                    Value = claim.Value
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(normalizedName, nameof(normalizedName));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetRoleNormalizedRoleNameCommand {
                    RoleId = role.Id,
                    NormalizedName = normalizedName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetRoleRoleNameCommand {
                    RoleId = role.Id,
                    Name = roleName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken) {
            Prevent.ParameterNull(role, nameof(role));

            return Task.Run(() => {
                try {
                    _commandDispatcher.Command(new UpdateRoleCommand {
                        RoleId = role.Id,
                        ConcurrencyStamp = role.ConcurrencyStamp,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName
                    });
                } catch (Exception ex) {
                    return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                }

                return IdentityResult.Success;
            }, cancellationToken);
        }

        #endregion IRoleStore<Role> Members
    }
}