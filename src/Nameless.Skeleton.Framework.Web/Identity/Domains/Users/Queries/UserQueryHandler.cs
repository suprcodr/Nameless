using System;
using System.Collections.Generic;
using System.Data;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Resources;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class UserQueryHandler : IQueryHandler<FindUserByEmailQuery, User>,
                                    IQueryHandler<FindUserByIdQuery, User>,
                                    IQueryHandler<FindUserByNormalizedUserNameQuery, User>,
                                    IQueryHandler<GetUserAccessFailedCountQuery, int>,
                                    IQueryHandler<GetUserEmailConfirmedQuery, bool>,
                                    IQueryHandler<GetUserEmailQuery, string>,
                                    IQueryHandler<GetUserIdQuery, Guid>,
                                    IQueryHandler<GetUserLockoutEnabledQuery, bool>,
                                    IQueryHandler<GetUserLockoutEndDateQuery, DateTimeOffset?>,
                                    IQueryHandler<GetUserNormalizedEmailQuery, string>,
                                    IQueryHandler<GetUserNormalizedUserNameQuery, string>,
                                    IQueryHandler<GetUserPasswordHashQuery, string>,
                                    IQueryHandler<GetUserPhoneNumberConfirmedQuery, bool>,
                                    IQueryHandler<GetUserPhoneNumberQuery, string>,
                                    IQueryHandler<GetUserSecurityStampQuery, string>,
                                    IQueryHandler<GetUsersFromClaimQuery, IEnumerable<User>>,
                                    IQueryHandler<GetUserTwoFactorEnabledQuery, bool>,
                                    IQueryHandler<GetUserUserNameQuery, string>,
                                    IQueryHandler<UserHasPasswordHashQuery, bool> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserQueryHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region IQueryHandler<FindUserByEmailQuery> Members

        public User Handle(FindUserByEmailQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.FindUserByEmail, Mappers.FindUserByEmail, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.NormalizedEmail), query.NormalizedEmail, DbType.Guid)
            });
        }

        #endregion IQueryHandler<FindUserByEmailQuery> Members

        #region IQueryHandler<FindUserByIdQuery> Members

        public User Handle(FindUserByIdQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.FindUserById, Mappers.FindUserById, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<FindUserByIdQuery> Members

        #region IQueryHandler<FindUserByNormalizedUserNameQuery> Members

        public User Handle(FindUserByNormalizedUserNameQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.FindUserByNormalizedUserName, Mappers.FindUserByNormalizedUserName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.NormalizedUserName), query.NormalizedUserName, DbType.Guid)
            });
        }

        #endregion IQueryHandler<FindUserByNormalizedUserNameQuery> Members

        #region IQueryHandler<GetUserAccessFailedCountQuery> Members

        public int Handle(GetUserAccessFailedCountQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserAccessFailedCount, Mappers.GetUserAccessFailedCount, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserAccessFailedCountQuery> Members

        #region IQueryHandler<GetUserEmailConfirmedQuery> Members

        public bool Handle(GetUserEmailConfirmedQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserEmailConfirmed, Mappers.GetUserEmailConfirmed, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserEmailConfirmedQuery> Members

        #region IQueryHandler<GetUserEmailQuery> Members

        public string Handle(GetUserEmailQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserEmail, Mappers.GetUserEmail, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserEmailQuery> Members

        #region IQueryHandler<GetUserIdQuery> Members

        public Guid Handle(GetUserIdQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserId, Mappers.GetUserId, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.NormalizedUserName), query.NormalizedUserName, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserIdQuery> Members

        #region IQueryHandler<GetUserLockoutEnabledQuery> Members

        public bool Handle(GetUserLockoutEnabledQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserLockoutEnabled, Mappers.GetUserLockoutEnabled, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserLockoutEnabledQuery> Members

        #region IQueryHandler<GetUserLockoutEndDateQuery> Members

        public DateTimeOffset? Handle(GetUserLockoutEndDateQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserLockoutEndDate, Mappers.GetUserLockoutEndDate, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserLockoutEndDateQuery> Members

        #region IQueryHandler<GetUserNormalizedEmailQuery> Members

        public string Handle(GetUserNormalizedEmailQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserNormalizedEmail, Mappers.GetUserNormalizedEmail, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserNormalizedEmailQuery> Members

        #region IQueryHandler<GetUserNormalizedUserNameQuery> Members

        public string Handle(GetUserNormalizedUserNameQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserNormalizedUserName, Mappers.GetUserNormalizedUserName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserNormalizedUserNameQuery> Members

        #region IQueryHandler<GetUserPasswordHashQuery> Members

        public string Handle(GetUserPasswordHashQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserPasswordHash, Mappers.GetUserPasswordHash, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserPasswordHashQuery> Members

        #region IQueryHandler<GetUserPhoneNumberConfirmedQuery> Members

        public bool Handle(GetUserPhoneNumberConfirmedQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserPhoneNumberConfirmed, Mappers.GetUserPhoneNumberConfirmed, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserPhoneNumberConfirmedQuery> Members

        #region IQueryHandler<GetUserPhoneNumberQuery> Members

        public string Handle(GetUserPhoneNumberQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserPhoneNumber, Mappers.GetUserPhoneNumber, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserPhoneNumberQuery> Members

        #region IQueryHandler<GetUserSecurityStampQuery> Members

        public string Handle(GetUserSecurityStampQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserSecurityStamp, Mappers.GetUserSecurityStamp, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserSecurityStampQuery> Members

        #region IQueryHandler<GetUsersFromClaimQuery> Members

        public IEnumerable<User> Handle(GetUsersFromClaimQuery query) {
            return _database.ExecuteReader((string)SQL.Instance.GetUsersFromClaim, Mappers.GetUsersFromClaim, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.Claim.Type), query.Claim.Type),
                Parameter.CreateInputParameter(nameof(query.Claim.Value), query.Claim.Value)
            });
        }

        #endregion IQueryHandler<GetUsersFromClaimQuery> Members

        #region IQueryHandler<GetUserTwoFactorEnabledQuery> Members

        public bool Handle(GetUserTwoFactorEnabledQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserTwoFactorEnabled, Mappers.GetUserTwoFactorEnabled, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserTwoFactorEnabledQuery> Members

        #region IQueryHandler<GetUserUserNameQuery> Members

        public string Handle(GetUserUserNameQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetUserUserName, Mappers.GetUserUserName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetUserUserNameQuery> Members

        #region IQueryHandler<UserHasPasswordHashQuery> Members

        public bool Handle(UserHasPasswordHashQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.UserHasPasswordHash, Mappers.UserHasPasswordHash, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<UserHasPasswordHashQuery> Members
    }
}