using System;
using System.Data;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.Web.Identity.Domains.Resources;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class UserCommandHandler : ICommandHandler<CreateUserCommand>,
                                      ICommandHandler<DeleteUserCommand>,
                                      ICommandHandler<IncrementUserAccessFailedCountCommand>,
                                      ICommandHandler<ResetUserAccessFailedCountCommand>,
                                      ICommandHandler<SetUserEmailCommand>,
                                      ICommandHandler<SetUserEmailConfirmedCommand>,
                                      ICommandHandler<SetUserLockoutEnabledCommand>,
                                      ICommandHandler<SetUserLockoutEndDateCommand>,
                                      ICommandHandler<SetUserNormalizedEmailCommand>,
                                      ICommandHandler<SetUserNormalizedUserNameCommand>,
                                      ICommandHandler<SetUserPasswordHashCommand>,
                                      ICommandHandler<SetUserPhoneNumberCommand>,
                                      ICommandHandler<SetUserPhoneNumberConfirmedCommand>,
                                      ICommandHandler<SetUserSecurityStampCommand>,
                                      ICommandHandler<SetUserTwoFactorEnabledCommand>,
                                      ICommandHandler<SetUserUserNameCommand>,
                                      ICommandHandler<UpdateUserCommand> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserCommandHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region ICommandHandler<CreateUserCommand> Members

        public void Handle(CreateUserCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.CreateUser, parameters: new[] {
                Parameter.CreateInputParameter("UserId", Guid.NewGuid(), DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.UserName), message.UserName),
                Parameter.CreateInputParameter(nameof(message.Email), message.Email),
                Parameter.CreateInputParameter(nameof(message.FullName), message.FullName),
                Parameter.CreateInputParameter(nameof(message.ProfilePicture), message.ProfilePicture)
            });
        }

        #endregion ICommandHandler<CreateUserCommand> Members

        #region ICommandHandler<DeleteUserCommand> Members

        public void Handle(DeleteUserCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.DeleteUser, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid)
            });
        }

        #endregion ICommandHandler<DeleteUserCommand> Members

        #region ICommandHandler<IncrementUserAccessFailedCountCommand> Members

        public void Handle(IncrementUserAccessFailedCountCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.IncrementUserAccessFailedCount, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid)
            });
        }

        #endregion ICommandHandler<IncrementUserAccessFailedCountCommand> Members

        #region ICommandHandler<ResetUserAccessFailedCountCommand> Members

        public void Handle(ResetUserAccessFailedCountCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.ResetUserAccessFailedCount, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid)
            });
        }

        #endregion ICommandHandler<ResetUserAccessFailedCountCommand> Members

        #region ICommandHandler<SetUserEmailCommand> Members

        public void Handle(SetUserEmailCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserEmail, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Email), message.Email)
            });
        }

        #endregion ICommandHandler<SetUserEmailCommand> Members

        #region ICommandHandler<SetUserEmailConfirmedCommand> Members

        public void Handle(SetUserEmailConfirmedCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserEmailConfirmed, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Confirmed), message.Confirmed, DbType.Boolean)
            });
        }

        #endregion ICommandHandler<SetUserEmailConfirmedCommand> Members

        #region ICommandHandler<SetUserLockoutEnabledCommand> Members

        public void Handle(SetUserLockoutEnabledCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserLockoutEnabled, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Enabled), message.Enabled, DbType.Boolean)
            });
        }

        #endregion ICommandHandler<SetUserLockoutEnabledCommand> Members

        #region ICommandHandler<SetUserLockoutEndDateCommand> Members

        public void Handle(SetUserLockoutEndDateCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserLockoutEndDate, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.LockoutEndDate), message.LockoutEndDate, DbType.DateTimeOffset)
            });
        }

        #endregion ICommandHandler<SetUserLockoutEndDateCommand> Members

        #region ICommandHandler<SetUserNormalizedEmailCommand> Members

        public void Handle(SetUserNormalizedEmailCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserNormalizedEmail, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.NormalizedEmail), message.NormalizedEmail)
            });
        }

        #endregion ICommandHandler<SetUserNormalizedEmailCommand> Members

        #region ICommandHandler<SetUserNormalizedUserNameCommand> Members

        public void Handle(SetUserNormalizedUserNameCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserNormalizedUserName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.NormalizedUserName), message.NormalizedUserName)
            });
        }

        #endregion ICommandHandler<SetUserNormalizedUserNameCommand> Members

        #region ICommandHandler<SetUserPasswordHashCommand> Members

        public void Handle(SetUserPasswordHashCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserPasswordHash, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.PasswordHash), message.PasswordHash)
            });
        }

        #endregion ICommandHandler<SetUserPasswordHashCommand> Members

        #region ICommandHandler<SetUserPhoneNumberCommand> Members

        public void Handle(SetUserPhoneNumberCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserPhoneNumber, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.PhoneNumber), message.PhoneNumber)
            });
        }

        #endregion ICommandHandler<SetUserPhoneNumberCommand> Members

        #region ICommandHandler<SetUserPhoneNumberConfirmedCommand> Members

        public void Handle(SetUserPhoneNumberConfirmedCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserPhoneNumberConfirmed, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Confirmed), message.Confirmed, DbType.Boolean)
            });
        }

        #endregion ICommandHandler<SetUserPhoneNumberConfirmedCommand> Members

        #region ICommandHandler<SetUserSecurityStampCommand> Members

        public void Handle(SetUserSecurityStampCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserSecurityStamp, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.SecurityStamp), message.SecurityStamp)
            });
        }

        #endregion ICommandHandler<SetUserSecurityStampCommand> Members

        #region ICommandHandler<SetUserTwoFactorEnabledCommand> Members

        public void Handle(SetUserTwoFactorEnabledCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserTwoFactorEnabled, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.Enabled), message.Enabled, DbType.Boolean)
            });
        }

        #endregion ICommandHandler<SetUserTwoFactorEnabledCommand> Members

        #region ICommandHandler<SetUserUserNameCommand> Members

        public void Handle(SetUserUserNameCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.SetUserUserName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.UserName), message.UserName)
            });
        }

        #endregion ICommandHandler<SetUserUserNameCommand> Members

        #region ICommandHandler<UpdateUserCommand> Members

        public void Handle(UpdateUserCommand message) {
            _database.ExecuteNonQuery(SQL.Instance.UpdateUser, parameters: new[] {
                Parameter.CreateInputParameter(nameof(message.UserId), message.UserId, DbType.Guid),
                Parameter.CreateInputParameter(nameof(message.FullName), message.FullName),
                Parameter.CreateInputParameter(nameof(message.ProfilePicture), message.ProfilePicture)
            });
        }

        #endregion ICommandHandler<UpdateUserCommand> Members
    }
}