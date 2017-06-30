using System;
using System.Data;
using Nameless.Framework.Data.Sql.Ado;
using Nameless.Framework.Web.Identity.Domains.Resources;

namespace Nameless.Framework.Web.Identity.Data.RoleClaims {

    public class AddClaimToRole : SqlCommand {

        #region Public Properties

        public string Type { get; set; }
        public string Value { get; set; }
        public Guid RoleID { get; set; }

        #endregion Public Properties

        #region Protected Override Properties

        protected override string Sql => SQL.Instance.AddClaimToRole;
        protected override CommandType CommandType => CommandType.Text;

        protected override Parameter[] Parameters => new[] {
            Parameter.CreateInputParameter(DatabaseSchema.RolesClaims.RoleClaimID, Guid.NewGuid(), DbType.Guid),
            Parameter.CreateInputParameter(DatabaseSchema.RolesClaims.Type, Type),
            Parameter.CreateInputParameter(DatabaseSchema.RolesClaims.Value, Value),
            Parameter.CreateInputParameter(DatabaseSchema.RolesClaims.RoleID, RoleID, DbType.Guid)
        };

        #endregion Protected Override Properties
    }
}