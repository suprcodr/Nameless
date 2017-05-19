using System;
using Nameless.Framework.Cqrs.Query;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Queries {

    public class GetRoleNormalizedRoleNameQuery : IQuery<string> {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}