using System;
using System.Data;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.Web.Identity.Ado.Resources;

namespace Nameless.Framework.Web.Identity.Ado.RoleClaims {

    public sealed class ReplaceClaimRole {

        #region Public Properties

        public Guid RoleID { get; set; }

        public string OldType { get; set; }
        public string OldValue { get; set; }

        public string NewType { get; set; }
        public string NewValue { get; set; }

        #endregion Public Properties
    }

    public sealed class ReplaceClaimRoleSqlStatement : SqlStatementBase<ReplaceClaimRole> {

        #region Public Constructors

        public ReplaceClaimRoleSqlStatement() {
            ActionType = ActionType.Save;
            Sql = SQL.Instance.ReplaceClaimRole;
            Type = CommandType.Text;
        }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Prepare(ReplaceClaimRole entity = null, params Parameter[] extraParameters) {
            AddParameter(string.Concat("old_", DatabaseSchema.RolesClaims.Type), entity.OldType);
            AddParameter(string.Concat("old_", DatabaseSchema.RolesClaims.Value), entity.OldValue);
            AddParameter(string.Concat("new_", DatabaseSchema.RolesClaims.Type), entity.NewType);
            AddParameter(string.Concat("new_", DatabaseSchema.RolesClaims.Value), entity.NewValue);
            AddParameter(DatabaseSchema.RolesClaims.RoleID, entity.RoleID, DbType.Guid);

            base.Prepare(extraParameters: extraParameters);
        }

        #endregion Public Override Methods
    }
}