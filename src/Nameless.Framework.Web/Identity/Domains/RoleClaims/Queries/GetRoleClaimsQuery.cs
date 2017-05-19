using System;
using System.Collections.Generic;
using Nameless.Framework.Cqrs.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Queries {

    public class GetRoleClaimsQuery : IQuery<IEnumerable<RoleClaim>> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}