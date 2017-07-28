using System;
using System.Collections.Generic;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Queries {

    public class GetRoleClaimsQuery : IQuery<IEnumerable<RoleClaim>> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}