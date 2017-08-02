using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.CQRS.Query;
using Nameless.WebApplication.Core.Identity.Domains.Users.Commands;
using Nameless.WebApplication.Core.Identity.Domains.Users.Queries;
using Nameless.WebApplication.Core.Identity.Domains.UsersClaims.Commands;
using Nameless.WebApplication.Core.Identity.Domains.UsersClaims.Queries;
using Nameless.WebApplication.Core.Identity.Domains.UsersLogins.Commands;
using Nameless.WebApplication.Core.Identity.Domains.UsersLogins.Queries;
using Nameless.WebApplication.Core.Identity.Models;
using Resource = Nameless.WebApplication.Core.Properties.Resources;

namespace Nameless.WebApplication.Core.Identity.Stores {

    /// <summary>
    /// Current implementation of <see cref="IUserStore{User}"/> and others.
    /// </summary>
    public sealed class UserStore : IQueryableUserStore<User>,
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

            return _queryDispatcher.QueryAsync(new GetUserClaimsQuery {
                UserID = user.ID
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(claims, nameof(claims));

            if (claims.IsNullOrEmpty()) { return Task.FromResult(0); }

            return _commandDispatcher.CommandAsync(new AddUserClaimsCommand {
                UserID = user.ID,
                Claims = claims.ToDictionary(_ => _.Type, _ => _.Value)
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(claim, nameof(claim));
            Prevent.ParameterNull(newClaim, nameof(newClaim));

            return _commandDispatcher.CommandAsync(new ReplaceUserClaimCommand {
                UserID = user.ID,
                OldClaimType = claim.Type,
                NewClaimType = newClaim.Type,
                NewClaimValue = newClaim.Value
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(claims, nameof(claims));

            return _commandDispatcher.CommandAsync(new RemoveUserClaimsCommand {
                UserID = user.ID,
                Claims = claims.ToDictionary(_ => _.Type, _ => _.Value)
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken) {
            Prevent.ParameterNull(claim, nameof(claim));

            return _queryDispatcher.QueryAsync(new GetUsersForClaimQuery {
                ClaimType = claim.Type
            }, cancellationToken);
        }

        #endregion IUserClaimStore<User> Members

        #region IUserEmailStore<User>

        /// <inheritdoc />
        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(email, nameof(email));

            cancellationToken.ThrowIfCancellationRequested();

            user.Email = email;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.Email);
        }

        /// <inheritdoc />
        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.EmailConfirmed);
        }

        /// <inheritdoc />
        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.EmailConfirmed = confirmed;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(normalizedEmail, nameof(normalizedEmail));

            return _queryDispatcher.QueryAsync(new FindUserByNormalizedEmailQuery {
                NormalizedEmail = normalizedEmail
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedEmail);
        }

        /// <inheritdoc />
        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(normalizedEmail, nameof(normalizedEmail));

            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedEmail = normalizedEmail;

            return Task.CompletedTask;
        }

        #endregion IUserEmailStore<User>

        #region IUserLockoutStore<User>

        /// <inheritdoc />
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.LockoutEndDateUtc);
        }

        /// <inheritdoc />
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.LockoutEndDateUtc = lockoutEnd;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.AccessFailedCount++;

            return Task.FromResult(user.AccessFailedCount);
        }

        /// <inheritdoc />
        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.AccessFailedCount = 0;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.AccessFailedCount);
        }

        /// <inheritdoc />
        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.LockoutEnabled);
        }

        /// <inheritdoc />
        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.LockoutEnabled = enabled;

            return Task.CompletedTask;
        }

        #endregion IUserLockoutStore<User>

        #region IUserLoginStore<User>

        /// <inheritdoc />
        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(login, nameof(login));

            return _commandDispatcher.CommandAsync(new AddUserLoginCommand {
                UserID = user.ID,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(loginProvider, nameof(loginProvider));
            Prevent.ParameterNullOrWhiteSpace(providerKey, nameof(providerKey));

            return _commandDispatcher.CommandAsync(new RemoveUserLoginCommand {
                UserID = user.ID,
                LoginProvider = loginProvider
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return _queryDispatcher.QueryAsync(new GetUserLoginsQuery {
                UserID = user.ID
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(loginProvider, nameof(loginProvider));
            Prevent.ParameterNullOrWhiteSpace(providerKey, nameof(providerKey));

            return _queryDispatcher.QueryAsync(new FindUserByLoginQuery {
                LoginProvider = loginProvider,
                ProviderKey = providerKey
            }, cancellationToken);
        }

        #endregion IUserLoginStore<User>

        #region IUserPasswordStore<User> Members

        /// <inheritdoc />
        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested();

            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(passwordHash, nameof(passwordHash));

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PasswordHash);
        }

        /// <inheritdoc />
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion IUserPasswordStore<User> Members

        #region IUserPhoneNumberStore<User> Members

        /// <inheritdoc />
        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(phoneNumber, nameof(phoneNumber));

            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumber = phoneNumber;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PhoneNumber);
        }

        /// <inheritdoc />
        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <inheritdoc />
        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.PhoneNumberConfirmed = confirmed;

            return Task.CompletedTask;
        }

        #endregion IUserPhoneNumberStore<User> Members

        #region IUserSecurityStampStore<User> Members

        /// <inheritdoc />
        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(stamp, nameof(stamp));

            cancellationToken.ThrowIfCancellationRequested();

            user.SecurityStamp = stamp;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion IUserSecurityStampStore<User> Members

        #region IUserStore<User> Members

        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return _commandDispatcher.CommandAsync(new CreateUserCommand {
                Email = user.Email,
                FullName = user.FullName,
                ProfilePicturePath = user.ProfilePicturePath,
                ProfilePictureBlob = user.ProfilePictureBlob,
                UserName = user.UserName
            }, cancellationToken: cancellationToken)
            .ContinueWith(IdentityResultContinuation);
        }

        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return _commandDispatcher.CommandAsync(new DeleteUserCommand {
                UserID = user.ID
            }, cancellationToken: cancellationToken)
              .ContinueWith(IdentityResultContinuation);
        }

        /// <inheritdoc />
        public void Dispose() {
        }

        /// <inheritdoc />
        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(userId, nameof(userId));

            return _queryDispatcher.QueryAsync(new FindUserByIDQuery {
                UserID = Guid.Parse(userId)
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            Prevent.ParameterNullOrWhiteSpace(normalizedUserName, nameof(normalizedUserName));

            return _queryDispatcher.QueryAsync(new FindUserByNormalizedUserNameQuery {
                NormalizedUserName = normalizedUserName
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.NormalizedUserName);
        }

        /// <inheritdoc />
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.ID.ToString());
        }

        /// <inheritdoc />
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.UserName);
        }

        /// <inheritdoc />
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(normalizedName, nameof(normalizedName));

            cancellationToken.ThrowIfCancellationRequested();

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNullOrWhiteSpace(userName, nameof(userName));

            cancellationToken.ThrowIfCancellationRequested();

            user.UserName = userName;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return _commandDispatcher.CommandAsync(new UpdateUserCommand {
                UserID = user.ID,
                FullName = user.FullName,
                ProfilePicture = user.ProfilePicturePath
            }, cancellationToken: cancellationToken)
            .ContinueWith(IdentityResultContinuation);
        }

        #endregion IUserStore<User> Members

        #region IUserTwoFactorStore<User> Members

        /// <inheritdoc />
        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <inheritdoc />
        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            cancellationToken.ThrowIfCancellationRequested();

            user.TwoFactorEnabled = enabled;

            return Task.CompletedTask;
        }

        #endregion IUserTwoFactorStore<User> Members

        #region IUserRoleStore<User> Members

        /// <inheritdoc />
        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return _commandDispatcher.CommandAsync(new AddUserToRoleCommand {
                UserID = user.ID,
                RoleName = roleName
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return _commandDispatcher.CommandAsync(new RemoveUserFromRoleCommand {
                UserID = user.ID,
                RoleName = roleName
            }, cancellationToken: cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));

            return _queryDispatcher.QueryAsync(new GetUserRolesQuery {
                UserID = user.ID
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(user, nameof(user));
            Prevent.ParameterNull(roleName, nameof(roleName));

            return _queryDispatcher.QueryAsync(new IsUserInRoleQuery {
                UserID = user.ID,
                RoleName = roleName
            }, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken) {
            Prevent.ParameterNull(roleName, nameof(roleName));

            return _queryDispatcher.QueryAsync(new GetUsersInRoleQuery {
                RoleName = roleName
            }, cancellationToken);
        }

        #endregion IUserRoleStore<User> Members
    }
}