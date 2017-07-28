using System.Collections.Generic;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUsersInRoleQuery : IQuery<IEnumerable<User>> {

        #region Public Properties

        public string RoleName { get; set; }

        #endregion Public Properties
    }
}