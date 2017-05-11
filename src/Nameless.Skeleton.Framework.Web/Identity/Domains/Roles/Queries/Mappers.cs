using System;
using System.Data;
using Nameless.Skeleton.Framework.Data.Sql.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Queries {

    internal static class Mappers {

        #region Internal Static Methods

        internal static Role FindRoleById(IDataReader reader) {
            return CommonMappers.ExtractRole(reader);
        }

        internal static Role FindRoleByName(IDataReader reader) {
            return CommonMappers.ExtractRole(reader);
        }

        internal static RoleClaim GetRoleClaims(IDataReader reader) {
            return CommonMappers.ExtractRoleClaim(reader);
        }

        internal static string GetRoleNormalizedRoleName(IDataReader reader) {
            return reader.GetStringOrDefault("normalized_name");
        }

        internal static Guid GetRoleRoleId(IDataReader reader) {
            return reader.GetGuidOrDefault("role_id");
        }

        internal static string GetRoleRoleName(IDataReader reader) {
            return reader.GetStringOrDefault("name");
        }

        #endregion Internal Static Methods
    }
}