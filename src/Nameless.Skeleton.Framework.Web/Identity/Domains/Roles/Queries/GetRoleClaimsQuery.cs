using System;
using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Queries {

    public class GetRoleClaimsQuery : IQuery<IEnumerable<RoleClaim>> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}