using System;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Queries {

    public class FindRoleByIdQuery : IQuery<Role> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}