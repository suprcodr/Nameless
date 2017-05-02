using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUsersInRoleQuery : IQuery<IEnumerable<User>> {

        #region Public Properties

        public string RoleName { get; set; }

        #endregion Public Properties
    }
}