using System;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Queries {

    public class GetRoleRoleNameQuery : IQuery<string> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}