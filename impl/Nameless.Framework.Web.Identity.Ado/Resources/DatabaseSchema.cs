namespace Nameless.Framework.Web.Identity.Ado.Resources {

    internal static class DatabaseSchema {

        #region Internal Static Inner Classes

        internal static class Users {

            #region Internal Static Read-Only Fields

            internal static readonly string UserID = "user_id";
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

            #endregion Internal Static Read-Only Fields
        }

        internal static class UsersClaims {

            #region Internal Static Read-Only Fields

            internal static readonly string UserClaimID = "user_claim_id";
            internal static readonly string Type = "type";
            internal static readonly string Value = "value";
            internal static readonly string UserID = "user_id";

            #endregion Internal Static Read-Only Fields
        }

        internal static class Roles {

            #region Internal Static Read-Only Fields

            internal static readonly string RoleID = "role_id";
            internal static readonly string ConcurrencyStamp = "concurrency_stamp";
            internal static readonly string Name = "name";
            internal static readonly string NormalizedName = "normalized_name";

            #endregion Internal Static Read-Only Fields
        }

        internal static class RolesClaims {

            #region Internal Static Read-Only Fields

            internal static readonly string RoleClaimID = "role_claim_id";
            internal static readonly string Type = "type";
            internal static readonly string Value = "value";
            internal static readonly string RoleID = "role_id";

            #endregion Internal Static Read-Only Fields
        }

        internal static class UsersLogins {

            #region Internal Static Read-Only Fields

            internal static readonly string UserID = "user_id";
            internal static readonly string LoginProvider = "login_provider";
            internal static readonly string ProviderKey = "provider_key";
            internal static readonly string DisplayName = "display_name";

            #endregion Internal Static Read-Only Fields
        }

        internal static class UsersTokens {

            #region Internal Static Read-Only Fields

            internal static readonly string UserID = "user_id";
            internal static readonly string LoginProvider = "login_provider";
            internal static readonly string Name = "name";
            internal static readonly string Value = "value";

            #endregion Internal Static Read-Only Fields
        }

        #endregion Internal Static Inner Classes
    }
}