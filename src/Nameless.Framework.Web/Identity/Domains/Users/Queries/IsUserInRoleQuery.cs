using System;
using Nameless.Framework.Cqrs.Query;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class IsUserInRoleQuery : IQuery<bool> {

        #region Public Properties

        public Guid UserId { get; set; }
        public string RoleName { get; set; }

        #endregion Public Properties
    }
}