using System;
using System.Data;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    internal static class Mappers {

        #region Internal Static Methods

        internal static User FindUserByEmail(IDataReader reader) {
            return CommonMappers.ExtractUser(reader);
        }

        internal static User FindUserById(IDataReader reader) {
            return CommonMappers.ExtractUser(reader);
        }

        internal static User FindUserByNormalizedUserName(IDataReader reader) {
            return CommonMappers.ExtractUser(reader);
        }

        internal static int GetUserAccessFailedCount(IDataReader reader) {
            return reader.GetInt32OrDefault("user_access_failed_count");
        }

        internal static bool GetUserEmailConfirmed(IDataReader reader) {
            return reader.GetBooleanOrDefault("email_confirmed");
        }

        internal static string GetUserEmail(IDataReader reader) {
            return reader.GetStringOrDefault("email");
        }

        internal static Guid GetUserId(IDataReader reader) {
            return reader.GetGuidOrDefault("user_id");
        }

        internal static bool GetUserLockoutEnabled(IDataReader reader) {
            return reader.GetBooleanOrDefault("lockout_enabled");
        }

        internal static DateTimeOffset? GetUserLockoutEndDate(IDataReader reader) {
            return reader.GetDateTimeOffsetOrDefault("lockout_end_date");
        }

        internal static string GetUserNormalizedEmail(IDataReader reader) {
            return reader.GetStringOrDefault("normalized_email");
        }

        internal static string GetUserNormalizedUserName(IDataReader reader) {
            return reader.GetStringOrDefault("normalized_user_name");
        }

        internal static string GetUserPasswordHash(IDataReader reader) {
            return reader.GetStringOrDefault("password_hash");
        }

        internal static bool GetUserPhoneNumberConfirmed(IDataReader reader) {
            return reader.GetBooleanOrDefault("phone_number_confirmed");
        }

        internal static string GetUserPhoneNumber(IDataReader reader) {
            return reader.GetStringOrDefault("phone_number");
        }

        internal static string GetUserSecurityStamp(IDataReader reader) {
            return reader.GetStringOrDefault("security_stamp");
        }

        internal static User GetUsersFromClaim(IDataReader reader) {
            return CommonMappers.ExtractUser(reader);
        }

        internal static bool GetUserTwoFactorEnabled(IDataReader reader) {
            return reader.GetBooleanOrDefault("two_factor_enabled");
        }

        internal static string GetUserUserName(IDataReader reader) {
            return reader.GetStringOrDefault("user_name");
        }

        internal static bool UserHasPasswordHash(IDataReader reader) {
            return reader.GetInt32OrDefault("has_password_hash") > 0;
        }

        #endregion Internal Static Methods
    }
}