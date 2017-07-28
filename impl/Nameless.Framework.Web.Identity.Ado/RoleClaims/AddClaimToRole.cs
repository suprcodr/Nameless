using System.Data;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.Web.Identity.Ado.Resources;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Ado.RoleClaims {

    public sealed class AddClaimToRoleSqlStatement : SqlStatementBase<RoleClaim> {

        #region Public Constructors

        public AddClaimToRoleSqlStatement() {
            ActionType = ActionType.Save;
            Sql = SQL.Instance.AddClaimToRole;
            Type = CommandType.Text;
        }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Prepare(RoleClaim entity = null, params Parameter[] extraParameters) {
            AddParameter(DatabaseSchema.RolesClaims.RoleClaimID, entity.ID, DbType.Guid);
            AddParameter(DatabaseSchema.RolesClaims.Type, entity.Type);
            AddParameter(DatabaseSchema.RolesClaims.Value, entity.Value);
            AddParameter(DatabaseSchema.RolesClaims.RoleID, entity.Role.ID, DbType.Guid);
        }

        #endregion Public Override Methods
    }
}