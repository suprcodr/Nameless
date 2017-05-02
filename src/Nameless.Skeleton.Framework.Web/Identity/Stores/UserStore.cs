using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Cqrs.Command;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Commands;
using Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Queries;
using Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Commands;
using Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Queries;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Commands;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Stores {

    /// <summary>
    /// Current implementation of <see cref="IUserStore{User}"/> and others.
    /// </summary>
    public class UserStore : IQueryableUserStore<User>,
                             IUserClaimStore<User>,
                             IUserEmailStore<User>,
                             IUserLockoutStore<User>,
                             IUserLoginStore<User>,
                             IUserPasswordStore<User>,
                             IUserPhoneNumberStore<User>,
                             IUserSecurityStampStore<User>,
                             IUserStore<User>,
                             IUserTwoFactorStore<User>,
                             IUserRoleStore<User> {

        #region Private Read-Only Fields

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserStore(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) {
            Prevent.ParameterNull(commandDispatcher, nameof(commandDispatcher));
            Prevent.ParameterNull(queryDispatcher, nameof(queryDispatcher));

            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        #endregion Public Constructors

        #region IQueryableUserStore<User> Members

        /// <inheritdoc />
        public IQueryable<User> Users {
            get { throw new NotImplementedException(); }
        }

        #endregion IQueryableUserStore<User> Members

        #region IUserClaimStore<User> Members

        /// <inheritdoc />
        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run<IList<Claim>>(() => {
                return _queryDispatcher.Query(new GetUserClaimsQuery {
                    UserId = user.Id
                })
                .Select(_ => _.ConvertFromUserClaim())
                .ToList();
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(claims, nameof(claims));

            if (claims.IsNullOrEmpty()) { return Task.FromResult(0); }

            return Task.Run(() => {
                _commandDispatcher.Command(new AddClaimsToUserCommand {
                    UserId = user.Id,
                    Claims = claims.Select(_ => _.ConvertToUserClaim())
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(claim, nameof(claim));
            Prevent.ParameterNull(newClaim, nameof(newClaim));

            return Task.Run(() => {
                _commandDispatcher.Command(new ReplaceUserClaimCommand {
                    UserId = user.Id,
                    OldClaim = new UserClaim { Type = claim.Type, Value = claim.Value },
                    NewClaim = new UserClaim { Type = newClaim.Type, Value = newClaim.Value }
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(claims, nameof(claims));

            return Task.Run(() => {
                _commandDispatcher.Command(new RemoveUserClaimsCommand {
                    UserId = user.Id,
                    Claims = claims.Select(_ => _.ConvertToUserClaim())
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken) {
            Prevent.ParameterNull(claim, nameof(claim));

            return Task.Run<IList<User>>(() => {
                return _queryDispatcher.Query(new GetUsersFromClaimQuery { Claim = claim.ConvertToUserClaim() })
                    .ToList();
            }, cancellationToken);
        }

        #endregion IUserClaimStore<User> Members

        #region IUserEmailStore<User>

        /// <inheritdoc />
        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(email, nameof(email));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserEmailCommand { UserId = user.Id, Email = email });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserEmailQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserEmailConfirmedQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserEmailConfirmedCommand { UserId = user.Id, Confirmed = confirmed });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(normalizedEmail, nameof(normalizedEmail));

            return Task.Run(() => {
                return _queryDispatcher.Query(new FindUserByEmailQuery { NormalizedEmail = normalizedEmail });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserNormalizedEmailQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(normalizedEmail, nameof(normalizedEmail));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserNormalizedEmailCommand { UserId = user.Id, NormalizedEmail = normalizedEmail });
            }, cancellationToken);
        }

        #endregion IUserEmailStore<User>

        #region IUserLockoutStore<User>

        /// <inheritdoc />
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserLockoutEndDateQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserLockoutEndDateCommand {
                    UserId = user.Id,
                    LockoutEndDate = lockoutEnd
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new IncrementUserAccessFailedCountCommand { UserId = user.Id });

                return _queryDispatcher.Query(new GetUserAccessFailedCountQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new ResetUserAccessFailedCountCommand { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserAccessFailedCountQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserLockoutEnabledQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserLockoutEnabledCommand {
                    UserId = user.Id,
                    Enabled = enabled
                });
            }, cancellationToken);
        }

        #endregion IUserLockoutStore<User>

        #region IUserLoginStore<User>

        /// <inheritdoc />
        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(login, nameof(login));

            return Task.Run(() => {
                _commandDispatcher.Command(new AddLoginToUserCommand {
                    UserId = user.Id,
                    LoginProvider = login.LoginProvider,
                    ProviderKey = login.ProviderKey
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(loginProvider, nameof(loginProvider));
            Prevent.ParameterNullOrWhiteSpace(providerKey, nameof(providerKey));

            return Task.Run(() => {
                _commandDispatcher.Command(new RemoveLoginFromUserCommand {
                    UserId = user.Id,
                    LoginProvider = loginProvider
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run<IList<UserLoginInfo>>(() => {
                return _queryDispatcher
                    .Query(new GetUserLoginsQuery { UserId = user.Id })
                    .Select(_ => _.ConvertFromUserLogin())
                    .ToList();
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(loginProvider, nameof(loginProvider));
            Prevent.ParameterNullOrWhiteSpace(providerKey, nameof(providerKey));

            return Task.Run(() => {
                return _queryDispatcher.Query(new FindUserByLoginQuery { LoginProvider = loginProvider, ProviderKey = providerKey });
            }, cancellationToken);
        }

        #endregion IUserLoginStore<User>

        #region IUserPasswordStore<User> Members

        /// <inheritdoc />
        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(passwordHash, nameof(passwordHash));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserPasswordHashCommand {
                    UserId = user.Id,
                    PasswordHash = passwordHash
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserPasswordHashQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new UserHasPasswordHashQuery { UserId = user.Id });
            }, cancellationToken);
        }

        #endregion IUserPasswordStore<User> Members

        #region IUserPhoneNumberStore<User> Members

        /// <inheritdoc />
        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(phoneNumber, nameof(phoneNumber));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserPhoneNumberCommand {
                    UserId = user.Id,
                    PhoneNumber = phoneNumber
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserPhoneNumberQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserPhoneNumberConfirmedQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserPhoneNumberConfirmedCommand {
                    UserId = user.Id,
                    Confirmed = confirmed
                });
            }, cancellationToken);
        }

        #endregion IUserPhoneNumberStore<User> Members

        #region IUserSecurityStampStore<User> Members

        /// <inheritdoc />
        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(stamp, nameof(stamp));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserSecurityStampCommand {
                    UserId = user.Id,
                    SecurityStamp = stamp
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserSecurityStampQuery { UserId = user.Id });
            }, cancellationToken);
        }

        #endregion IUserSecurityStampStore<User> Members

        #region IUserStore<User> Members

        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new CreateUserCommand {
                    Email = user.Email,
                    FullName = user.FullName,
                    ProfilePicture = user.ProfilePicture,
                    UserName = user.UserName
                });

                return IdentityResult.Success;
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new DeleteUserCommand {
                    UserId = user.Id
                });

                return IdentityResult.Success;
            }, cancellationToken);
        }

        /// <inheritdoc />
        public void Dispose() {
        }

        /// <inheritdoc />
        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(userId, nameof(userId));

            return Task.Run(() => {
                return _queryDispatcher.Query(new FindUserByIdQuery { UserId = Guid.Parse(userId) });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(normalizedUserName, nameof(normalizedUserName));

            return Task.Run(() => {
                return _queryDispatcher.Query(new FindUserByNormalizedUserNameQuery { NormalizedUserName = normalizedUserName });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserNormalizedUserNameQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher
                    .Query(new GetUserIdQuery { NormalizedUserName = user.NormalizedUserName })
                    .ToString();
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserUserNameQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(normalizedName, nameof(normalizedName));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserNormalizedUserNameCommand {
                    UserId = user.Id,
                    NormalizedUserName = normalizedName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(userName, nameof(userName));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserUserNameCommand {
                    UserId = user.Id,
                    UserName = userName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new UpdateUserCommand {
                    UserId = user.Id,
                    FullName = user.FullName,
                    ProfilePicture = user.ProfilePicture
                });

                return IdentityResult.Success;
            }, cancellationToken);
        }

        #endregion IUserStore<User> Members

        #region IUserTwoFactorStore<User> Members

        /// <inheritdoc />
        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                return _queryDispatcher.Query(new GetUserTwoFactorEnabledQuery { UserId = user.Id });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run(() => {
                _commandDispatcher.Command(new SetUserTwoFactorEnabledCommand {
                    UserId = user.Id,
                    Enabled = enabled
                });
            }, cancellationToken);
        }

        #endregion IUserTwoFactorStore<User> Members

        #region IUserRoleStore<User> Members

        /// <inheritdoc />
        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return Task.Run(() => {
                _commandDispatcher.Command(new AddUserToRoleCommand {
                    UserId = user.Id,
                    RoleName = roleName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return Task.Run(() => {
                _commandDispatcher.Command(new RemoveUserFromRoleCommand {
                    UserId = user.Id,
                    RoleName = roleName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return Task.Run<IList<string>>(() => {
                return _queryDispatcher.Query(new GetUserRolesQuery {
                    UserId = user.Id
                }).ToList();
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return Task.Run(() => {
                return _queryDispatcher.Query(new IsUserInRoleQuery {
                    UserId = user.Id,
                    RoleName = roleName
                });
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(roleName, nameof(roleName));

            return Task.Run<IList<User>>(() => {
                return _queryDispatcher.Query(new GetUsersInRoleQuery {
                    RoleName = roleName
                }).ToList();
            }, cancellationToken);
        }

        #endregion IUserRoleStore<User> Members
    }
}