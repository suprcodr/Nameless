using System;
using System.Data;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Helpers;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains {

    internal static class CommonMappers {

        #region Internal Static Methods

        internal static User ExtractUser(IDataReader reader) {
            var user = new User {
                AccessFailedCount = reader.GetInt32OrDefault("access_failed_count"),
                ConcurrencyStamp = reader.GetStringOrDefault("concurrency_stamp"),
                Email = reader.GetStringOrDefault("email"),
                EmailConfirmed = reader.GetBooleanOrDefault("email_confirmed"),
                FullName = reader.GetStringOrDefault("full_name"),
                LockoutEnabled = reader.GetBooleanOrDefault("lockout_enabled"),
                LockoutEndDateUtc = reader.GetDateTimeOffsetOrDefault("lockout_end_date_utc"),
                NormalizedEmail = reader.GetStringOrDefault("normalized_email"),
                NormalizedUserName = reader.GetStringOrDefault("normalized_user_name"),
                PasswordHash = reader.GetStringOrDefault("password_hash"),
                PhoneNumber = reader.GetStringOrDefault("phone_number"),
                PhoneNumberConfirmed = reader.GetBooleanOrDefault("phone_number_confirmed"),
                ProfilePicture = reader.GetStringOrDefault("profile_picture_path"),
                SecurityStamp = reader.GetStringOrDefault("security_stamp"),
                TwoFactorEnabled = reader.GetBooleanOrDefault("two_factor_enabled"),
                UserName = reader.GetStringOrDefault("user_name"),
                ProfilePictureBlob = reader.GetBlobOrDefault("profile_picture_blob", default(byte[])) // TODO : put default user image.
            };

            ReflectionHelper.SetPrivateFieldValue(user, "_id", reader.GetGuidOrDefault("user_id", Guid.Empty));

            return user;
        }

        internal static Role ExtractRole(IDataReader reader) {
            var role = new Role {
                ConcurrencyStamp = reader.GetStringOrDefault("concurrency_stamp"),
                Name = reader.GetStringOrDefault("name"),
                NormalizedName = reader.GetStringOrDefault("normalized_name")
            };

            ReflectionHelper.SetPrivateFieldValue(role, "_id", reader.GetGuidOrDefault("role_id", Guid.Empty));

            return role;
        }

        internal static UserClaim ExtractUserClaim(IDataReader reader) {
            var userClaim = new UserClaim {
                Type = reader.GetStringOrDefault("type"),
                Value = reader.GetStringOrDefault("value"),
                User = new User()
            };

            ReflectionHelper.SetPrivateFieldValue(userClaim, "_id", reader.GetGuidOrDefault("user_claim_id", Guid.Empty));
            ReflectionHelper.SetPrivateFieldValue(userClaim.User, "_id", reader.GetGuidOrDefault("user_id", Guid.Empty));

            return userClaim;
        }

        internal static RoleClaim ExtractRoleClaim(IDataReader reader) {
            var roleClaim = new RoleClaim {
                Type = reader.GetStringOrDefault("type"),
                Value = reader.GetStringOrDefault("value"),
                Role = new Role()
            };

            ReflectionHelper.SetPrivateFieldValue(roleClaim, "_id", reader.GetGuidOrDefault("role_claim_id", Guid.Empty));
            ReflectionHelper.SetPrivateFieldValue(roleClaim.Role, "_id", reader.GetGuidOrDefault("role_id", Guid.Empty));

            return roleClaim;
        }

        internal static UserLogin ExtractUserLogin(IDataReader reader) {
            var userLogin = new UserLogin {
                LoginProvider = reader.GetStringOrDefault("login_provider"),
                ProviderKey = reader.GetStringOrDefault("provider_key"),
                DisplayName = reader.GetStringOrDefault("display")
            };

            return userLogin;
        }

        #endregion Internal Static Methods
    }
}