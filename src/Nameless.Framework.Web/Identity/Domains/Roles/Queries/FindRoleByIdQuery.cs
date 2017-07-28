using System;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Queries {

    public class FindRoleByIdQuery : IQuery<Role> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}