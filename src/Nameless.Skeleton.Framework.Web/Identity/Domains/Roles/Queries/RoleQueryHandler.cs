using System;
using System.Collections.Generic;
using System.Data;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Resources;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Queries {

    public class RoleQueryHandler : IQueryHandler<FindRoleByIdQuery, Role>,
                                    IQueryHandler<FindRoleByNameQuery, Role>,
                                    IQueryHandler<GetRoleClaimsQuery, IEnumerable<RoleClaim>>,
                                    IQueryHandler<GetRoleNormalizedRoleNameQuery, string>,
                                    IQueryHandler<GetRoleRoleIdQuery, Guid>,
                                    IQueryHandler<GetRoleRoleNameQuery, string> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleQueryHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region IQueryHandler<FindRoleByIdQuery, Role> Members

        public Role Handle(FindRoleByIdQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.FindRoleById, Mappers.FindRoleById, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.RoleId), query.RoleId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<FindRoleByIdQuery, Role> Members

        #region IQueryHandler<FindRoleByNameQuery, Role> Members

        public Role Handle(FindRoleByNameQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.FindRoleByName, Mappers.FindRoleByName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.NormalizedName), query.NormalizedName)
            });
        }

        #endregion IQueryHandler<FindRoleByNameQuery, Role> Members

        #region IQueryHandler<GetRoleClaimsQuery, IEnumerable<RoleClaim>> Members

        public IEnumerable<RoleClaim> Handle(GetRoleClaimsQuery query) {
            return _database.ExecuteReader((string)SQL.Instance.GetRoleClaims, Mappers.GetRoleClaims, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.RoleId), query.RoleId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetRoleClaimsQuery, IEnumerable<RoleClaim>> Members

        #region IQueryHandler<GetRoleNormalizedRoleNameQuery, Role> Members

        public string Handle(GetRoleNormalizedRoleNameQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetRoleNormalizedRoleName, Mappers.GetRoleNormalizedRoleName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.RoleId), query.RoleId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetRoleNormalizedRoleNameQuery, Role> Members

        #region IQueryHandler<GetRoleRoleIdQuery, Role> Members

        public Guid Handle(GetRoleRoleIdQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetRoleRoleId, Mappers.GetRoleRoleId, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.NormalizedName), query.NormalizedName)
            });
        }

        #endregion IQueryHandler<GetRoleRoleIdQuery, Role> Members

        #region IQueryHandler<GetRoleRoleNameQuery, Role> Members

        public string Handle(GetRoleRoleNameQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.GetRoleRoleName, Mappers.GetRoleRoleName, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.RoleId), query.RoleId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<GetRoleRoleNameQuery, Role> Members
    }
}