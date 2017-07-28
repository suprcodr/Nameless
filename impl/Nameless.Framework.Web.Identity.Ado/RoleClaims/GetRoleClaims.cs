using System;
using System.Data;
using Nameless.Framework.Data.Generic.Sql;
using Nameless.Framework.Data.Generic.Sql.Ado;
using Nameless.Framework.Web.Identity.Ado.Resources;

namespace Nameless.Framework.Web.Identity.Ado.RoleClaims {

    public sealed class GetRoleClaims {

        #region Public Properties

        public Guid RoleClaimID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Guid RoleID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetRoleClaimsSqlStatement : SqlStatementBase<GetRoleClaims> {

        #region Public Constructors

        public GetRoleClaimsSqlStatement() {
            ActionType = ActionType.FindOneByExpression;
            Sql = SQL.Instance.GetRoleClaims;
            Type = CommandType.Text;
        }

        #endregion Public Constructors

        #region Public Override Methods

        public override void Prepare(GetRoleClaims entity = null, params Parameter[] extraParameters) {
            AddParameter(DatabaseSchema.RolesClaims.RoleID, entity.RoleID, DbType.Guid);

            base.Prepare(extraParameters: extraParameters);
        }

        #endregion Public Override Methods
    }

    public sealed class GetRoleClaimsSqlProjector : ISqlProjector<GetRoleClaims> {

        #region ISqlProjector<GetRoleClaims> Members

        public GetRoleClaims Map(IDataReader reader) {
            return new GetRoleClaims {
                RoleClaimID = reader.GetGuidOrDefault(DatabaseSchema.RolesClaims.RoleClaimID),
                Type = reader.GetStringOrDefault(DatabaseSchema.RolesClaims.Type),
                Value = reader.GetStringOrDefault(DatabaseSchema.RolesClaims.Value),
                RoleID = reader.GetGuidOrDefault(DatabaseSchema.RolesClaims.RoleID)
            };
        }

        #endregion ISqlProjector<GetRoleClaims> Members
    }
}