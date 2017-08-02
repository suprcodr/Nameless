using System;
using System.Data;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;
using Nameless.WebApplication.Core.Models;

namespace Nameless.WebApplication.Core {

    internal static class EntitySchema {

        #region Internal Static Classes

        internal static class Users {

            #region Internal Static Read-Only Fields

            internal static readonly string TableName = "users";

            #endregion Internal Static Read-Only Fields

            #region Internal Static Methods

            internal static User Mapper(IDataReader reader) {
                return new User(reader.GetGuidOrDefault(Fields.ID)) {
                    ConcurrencyStamp = reader.GetStringOrDefault(Fields.ConcurrencyStamp),
                    UserName = reader.GetStringOrDefault(Fields.UserName),
                    NormalizedUserName = reader.GetStringOrDefault(Fields.NormalizedUserName),
                    FullName = reader.GetStringOrDefault(Fields.FullName),
                    AccessFailedCount = reader.GetInt32OrDefault(Fields.AccessFailedCount),
                    Email = reader.GetStringOrDefault(Fields.Email),
                    EmailConfirmed = reader.GetBooleanOrDefault(Fields.EmailConfirmed),
                    NormalizedEmail = reader.GetStringOrDefault(Fields.NormalizedEmail),
                    LockoutEnabled = reader.GetBooleanOrDefault(Fields.LockoutEnabled),
                    LockoutEndDateUtc = reader.GetDateTimeOffsetOrDefault(Fields.LockoutEndDateUtc),
                    PasswordHash = reader.GetStringOrDefault(Fields.PasswordHash),
                    PhoneNumber = reader.GetStringOrDefault(Fields.PhoneNumber),
                    PhoneNumberConfirmed = reader.GetBooleanOrDefault(Fields.PhoneNumberConfirmed),
                    TwoFactorEnabled = reader.GetBooleanOrDefault(Fields.TwoFactorEnabled),
                    SecurityStamp = reader.GetStringOrDefault(Fields.SecurityStamp),
                    ProfilePicturePath = reader.GetStringOrDefault(Fields.ProfilePicturePath),
                    ProfilePictureBlob = reader.GetBlobOrDefault(Fields.ProfilePictureBlob)
                };
            }

            #endregion

            #region Internal Static Classes

            internal static class StoredProcedures {

                #region Internal Static Read-Only Fields

                // Alteration
                internal static readonly string AddUserToRole = nameof(AddUserToRole);
                internal static readonly string CreateUser = nameof(CreateUser);
                internal static readonly string DeleteUser = nameof(DeleteUser);
                internal static readonly string IncrementUserAccessFailedCount = nameof(IncrementUserAccessFailedCount);
                internal static readonly string RemoveUserFromRole = nameof(RemoveUserFromRole);
                internal static readonly string ResetUserAccessFailedCount = nameof(ResetUserAccessFailedCount);
                internal static readonly string SetUserEmail = nameof(SetUserEmail);
                internal static readonly string SetUserEmailConfirmed = nameof(SetUserEmailConfirmed);
                internal static readonly string SetUserLockoutEnabled = nameof(SetUserLockoutEnabled);
                internal static readonly string SetUserLockoutEndDate = nameof(SetUserLockoutEndDate);
                internal static readonly string SetUserName = nameof(SetUserName);
                internal static readonly string SetUserNormalizedName = nameof(SetUserNormalizedName);
                internal static readonly string SetUserPasswordHash = nameof(SetUserPasswordHash);
                internal static readonly string SetUserPhoneNumber = nameof(SetUserPhoneNumber);
                internal static readonly string SetUserPhoneNumberConfirmed = nameof(SetUserPhoneNumberConfirmed);
                internal static readonly string SetUserSecurityStamp = nameof(SetUserSecurityStamp);
                internal static readonly string SetUserTwoFactorEnabled = nameof(SetUserTwoFactorEnabled);
                internal static readonly string UpdateUser = nameof(UpdateUser);

                // Query
                internal static readonly string FindUserByID = nameof(FindUserByID);
                internal static readonly string FindUserByLogin = nameof(FindUserByLogin);
                internal static readonly string FindUserByNormalizedEmail = nameof(FindUserByNormalizedEmail);
                internal static readonly string FindUserByNormalizedUserName = nameof(FindUserByNormalizedUserName);
                internal static readonly string GetUserAccessFailedCount = nameof(GetUserAccessFailedCount);
                internal static readonly string GetUserEmail = nameof(GetUserEmail);
                internal static readonly string GetUserEmailConfirmed = nameof(GetUserEmailConfirmed);
                internal static readonly string GetUserID = nameof(GetUserID);
                internal static readonly string GetUserLockoutEnabled = nameof(GetUserLockoutEnabled);
                internal static readonly string GetUserLockoutEndDate = nameof(GetUserLockoutEndDate);
                internal static readonly string GetUserName = nameof(GetUserName);
                internal static readonly string GetUserNormalizedEmail = nameof(GetUserNormalizedEmail);
                internal static readonly string GetUserNormalizedName = nameof(GetUserNormalizedName);
                internal static readonly string GetUserPasswordHash = nameof(GetUserPasswordHash);
                internal static readonly string GetUserPhoneNumber = nameof(GetUserPhoneNumber);
                internal static readonly string GetUserPhoneNumberConfirmed = nameof(GetUserPhoneNumberConfirmed);
                internal static readonly string GetUserRoles = nameof(GetUserRoles);
                internal static readonly string GetUserSecurityStamp = nameof(GetUserSecurityStamp);
                internal static readonly string GetUsersForClaim = nameof(GetUsersForClaim);
                internal static readonly string GetUsersInRole = nameof(GetUsersInRole);
                internal static readonly string GetUserTwoFactorEnabled = nameof(GetUserTwoFactorEnabled);
                internal static readonly string IsUserInRole = nameof(IsUserInRole);
                internal static readonly string UserHasPassword = nameof(UserHasPassword);

                #endregion Internal Static Read-Only Fields
            }

            internal static class Constraints {

                #region Internal Static Read-Only Fields

                internal static readonly string PrimaryKey = "pk_users";

                internal static readonly string ForeignKey_Owner = "fk_users_to_owners";

                internal static readonly string Unique_UserName_Owner = "uq_users_user_name_owner";
                internal static readonly string Unique_Email_Owner = "uq_users_email_owner";

                internal static readonly string Index_UserName = "idx_users_user_name";
                internal static readonly string Index_FullName = "idx_users_full_name";
                internal static readonly string Index_Email = "idx_users_email";

                #endregion Internal Static Read-Only Fields
            }

            internal static class Fields {

                #region Internal Static Read-Only Fields

                internal static readonly string ID = "user_id";
                internal static readonly string ConcurrencyStamp = "concurrency_stamp";
                internal static readonly string UserName = "user_name";
                internal static readonly string NormalizedUserName = "normalized_user_name";
                internal static readonly string FullName = "full_name";
                internal static readonly string AccessFailedCount = "access_failed_count";
                internal static readonly string Email = "email";
                internal static readonly string EmailConfirmed = "email_confirmed";
                internal static readonly string NormalizedEmail = "normalized_email";
                internal static readonly string LockoutEnabled = "lockout_enabled";
                internal static readonly string LockoutEndDateUtc = "lockout_end_date_utc";
                internal static readonly string PasswordHash = "password_hash";
                internal static readonly string PhoneNumber = "phone_number";
                internal static readonly string PhoneNumberConfirmed = "phone_number_confirmed";
                internal static readonly string TwoFactorEnabled = "two_factor_enabled";
                internal static readonly string SecurityStamp = "security_stamp";
                internal static readonly string ProfilePicturePath = "profile_picture_path";
                internal static readonly string ProfilePictureBlob = "profile_picture_blob";
                internal static readonly string Attributes = "attributes";

                internal static readonly string OwnerID = "owner_id";

                #endregion Internal Static Read-Only Fields
            }

            #endregion Internal Static Classes
        }

        internal static class UsersClaims {

            #region Internal Static Read-Only Fields

            internal static readonly string TableName = "users_claims";

            #endregion Internal Static Read-Only Fields

            #region Internal Static Methods

            internal static UserClaim Mapper(IDataReader reader) {
                return new UserClaim(reader.GetGuidOrDefault(Fields.UserID)) {
                    Type = reader.GetStringOrDefault(Fields.Type),
                    Value = reader.GetStringOrDefault(Fields.Value)
                };
            }

            #endregion Internal Static Methods

            #region Internal Static Classes

            internal static class StoredProcedures {

                #region Internal Static Read-Only Fields

                // Alteration
                internal static readonly string AddUserClaim = nameof(AddUserClaim);
                internal static readonly string ReplaceUserClaim = nameof(ReplaceUserClaim);
                internal static readonly string RemoveUserClaim = nameof(RemoveUserClaim);

                // Query
                internal static readonly string GetUserClaims = nameof(GetUserClaims);

                #endregion Internal Static Read-Only Fields
            }

            internal static class Constraints {

                #region Internal Static Read-Only Fields

                internal static readonly string PrimaryKey = "pk_users_claims";

                internal static readonly string ForeignKey_Users = "fk_users_claims_to_users";

                internal static readonly string Index_Type = "idx_users_claims_type";

                #endregion Internal Static Read-Only Fields
            }

            internal static class Fields {

                #region Internal Static Read-Only Fields

                internal static readonly string UserID = "user_id";
                internal static readonly string Type = "type";
                internal static readonly string Value = "value";

                #endregion Internal Static Read-Only Fields
            }

            #endregion Internal Static Classes
        }

        internal static class UsersLogins {

            #region Internal Static Read-Only Fields

            internal static readonly string TableName = "users_logins";

            #endregion Internal Static Read-Only Fields

            #region Internal Static Methods

            internal static UserLogin Mapper(IDataReader reader) {
                return new UserLogin(reader.GetGuidOrDefault(Fields.UserID)) {
                    DisplayName = reader.GetStringOrDefault(Fields.DisplayName),
                    LoginProvider = reader.GetStringOrDefault(Fields.LoginProvider),
                    ProviderKey = reader.GetStringOrDefault(Fields.ProviderKey)
                };
            }

            #endregion Internal Static Methods

            #region Internal Static Classes

            internal static class StoredProcedures {

                #region Internal Static Read-Only Fields

                // Alteration
                internal static readonly string AddUserLogin = nameof(AddUserLogin);
                internal static readonly string RemoveUserLogin = nameof(RemoveUserLogin);

                // Query
                internal static readonly string GetUserLogins = nameof(GetUserLogins);

                #endregion Internal Static Read-Only Fields
            }

            internal static class Constraints {

                #region Internal Static Read-Only Fields

                internal static readonly string PrimaryKey = "pk_users_logins";

                internal static readonly string ForeignKey_Users = "fk_users_logins_to_users";
                
                internal static readonly string Index_LoginProvider = "idx_users_logins_login_provider";
                internal static readonly string Index_DisplayName = "idx_users_logins_display_name";

                #endregion Internal Static Read-Only Fields
            }

            internal static class Fields {

                #region Internal Static Read-Only Fields

                internal static readonly string UserID = "user_id";
                internal static readonly string LoginProvider = "login_provider";
                internal static readonly string ProviderKey = "provider_key";
                internal static readonly string DisplayName = "display_name";

                #endregion Internal Static Read-Only Fields
            }

            #endregion Internal Static Classes
        }

        internal static class Roles {

            #region Internal Static Read-Only Fields

            internal static readonly string TableName = "roles";

            #endregion Internal Static Read-Only Fields

            #region Internal Static Methods

            internal static Role Mapper(IDataReader reader) {
                return new Role(reader.GetGuidOrDefault(Fields.ID)) {
                    ConcurrencyStamp = reader.GetStringOrDefault(Fields.ConcurrencyStamp),
                    Name = reader.GetStringOrDefault(Fields.Name),
                    NormalizedName = reader.GetStringOrDefault(Fields.NormalizedName),
                    Owner = new Owner(reader.GetGuidOrDefault(Fields.OwnerID))
                };
            }

            #endregion Internal Static Methods

            #region Internal Static Classes

            internal static class StoredProcedures {

                #region Internal Static Read-Only Fields

                // Alteration
                internal static readonly string CreateRole = nameof(CreateRole);
                internal static readonly string DeleteRole = nameof(DeleteRole);
                internal static readonly string SetRoleName = nameof(SetRoleName);
                internal static readonly string SetRoleNormalizedName = nameof(SetRoleNormalizedName);
                internal static readonly string UpdateRole = nameof(UpdateRole);

                // Query
                internal static readonly string FindRoleByID = nameof(FindRoleByID);
                internal static readonly string FindRoleByNormalizedName = nameof(FindRoleByNormalizedName);
                internal static readonly string GetRoleID = nameof(GetRoleID);
                internal static readonly string GetRoleName = nameof(GetRoleName);
                internal static readonly string GetRoleNormalizedName = nameof(GetRoleNormalizedName);

                #endregion Internal Static Read-Only Fields
            }

            internal static class Constraints {

                #region Internal Static Read-Only Fields

                internal static readonly string PrimaryKey = "pk_roles";

                internal static readonly string ForeignKey_Owner = "fk_roles_to_owners";

                internal static readonly string Unique_Name_Owner = "uq_roles_name_owner";

                internal static readonly string Index_Name = "idx_roles_name";
                internal static readonly string Index_NormalizedName = "idx_roles_normalized_name";

                #endregion Internal Static Read-Only Fields
            }

            internal static class Fields {

                #region Internal Static Read-Only Fields

                internal static readonly string ID = "role_id";
                internal static readonly string ConcurrencyStamp = "concurrency_stamp";
                internal static readonly string Name = "name";
                internal static readonly string NormalizedName = "normalized_name";

                internal static readonly string OwnerID = "owner_id";

                #endregion Internal Static Read-Only Fields
            }

            #endregion Internal Static Classes
        }

        internal static class RolesClaims {

            #region Internal Static Read-Only Fields

            internal static readonly string TableName = "roles_claims";

            #endregion Internal Static Read-Only Fields

            #region Internal Static Methods

            internal static RoleClaim Mapper(IDataReader reader) {
                return new RoleClaim(reader.GetGuidOrDefault(Fields.RoleID)) {
                    Type = reader.GetStringOrDefault(Fields.Type),
                    Value = reader.GetStringOrDefault(Fields.Value)
                };
            }

            #endregion Internal Static Methods

            #region Internal Static Classes

            internal static class StoredProcedures {

                #region Internal Static Read-Only Fields

                // Alteration
                internal static readonly string AddClaimToRole = nameof(AddClaimToRole);
                internal static readonly string RemoveRoleClaim = nameof(RemoveRoleClaim);

                // Query
                internal static readonly string GetRoleClaims = nameof(GetRoleClaims);

                #endregion Internal Static Read-Only Fields
            }

            internal static class Constraints {

                #region Internal Static Read-Only Fields

                internal static readonly string ForeignKey_Role = "fk_roles_claims_to_roles";

                internal static readonly string Unique_Role_Type = "uq_roles_claims_role_type";

                internal static readonly string Index_Type = "idx_roles_claims_type";

                #endregion Internal Static Read-Only Fields
            }

            internal static class Fields {

                #region Internal Static Read-Only Fields

                internal static readonly string RoleID = "role_id";
                internal static readonly string Type = "type";
                internal static readonly string Value = "value";

                #endregion Internal Static Read-Only Fields
            }

            #endregion Internal Static Classes
        }

        internal static class Owners {

            #region Internal Static Read-Only Fields

            internal static readonly string TableName = "owners";

            #endregion Internal Static Read-Only Fields

            #region Internal Static Classes

            internal static class StoredProcedures {

                #region Internal Static Read-Only Fields

                // Alteration
                internal static readonly string FindOwners = nameof(FindOwners);
                internal static readonly string RemoveOwner = nameof(RemoveOwner);
                internal static readonly string SaveOwner = nameof(SaveOwner);

                // Query
                internal static readonly string GetOwner = nameof(GetOwner);
                internal static readonly string ListOwners = nameof(ListOwners);

                #endregion Internal Static Read-Only Fields
            }

            internal static class Constraints {

                #region Internal Static Read-Only Fields

                internal static readonly string PrimaryKey = "pk_owners";

                internal static readonly string Unique_Name = "uq_owners_name";

                internal static readonly string Index_Name = "idx_owners_name";

                #endregion Internal Static Read-Only Fields
            }

            internal static class Fields {

                #region Internal Static Read-Only Fields

                internal static readonly string ID = "owner_id";
                internal static readonly string Name = "name";

                #endregion Internal Static Read-Only Fields
            }

            #endregion Internal Static Classes
        }

        #endregion Internal Static Classes
    }
}