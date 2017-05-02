﻿using System.Collections.Generic;
using System.Data;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Resources;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.RoleClaims.Queries {

    public class RoleClaimQueryHandler : IQueryHandler<GetRoleClaimsQuery, IEnumerable<RoleClaim>> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleClaimQueryHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region IQueryHandler<ListRoleClaimsQuery, IEnumerable<RoleClaim>> Members

        public IEnumerable<RoleClaim> Handle(GetRoleClaimsQuery query) {
            return _database.ExecuteReader((string)SQL.Instance.GetRoleClaims, Mappers.GetRoleClaims, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.RoleId), query.RoleId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<ListRoleClaimsQuery, IEnumerable<RoleClaim>> Members
    }
}