using System;
using System.Data;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.Web.Identity.Ado.Resources;

namespace Nameless.Framework.Web.Identity.Ado.RoleClaims {

    public sealed class RemoveRoleClaims {

        #region Public Properties

        public string Type { get; set; }
        public string Value { get; set; }
        public Guid RoleID { get; set; }

        #endregion Public Properties
    }

    public sealed class RemoveRoleClaimsSqlStatement : SqlStatementBase<RemoveRoleClaims> {

        #region Public Constructors

        public RemoveRoleClaimsSqlStatement() {
            ActionType = ActionType.Save;
            Sql = SQL.Instance.RemoveRoleClaims;
            Type = CommandType.Text;
        }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Prepare(RemoveRoleClaims entity = null, params Parameter[] extraParameters) {
            AddParameter(DatabaseSchema.RolesClaims.Type, entity.Type);
            AddParameter(DatabaseSchema.RolesClaims.Value, entity.Value);
            AddParameter(DatabaseSchema.RolesClaims.RoleID, entity.RoleID, DbType.Guid);

            base.Prepare(extraParameters: extraParameters);
        }

        #endregion Public Override Methods
    }
}