using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Queries {

    public class FindRoleByNameQuery : IQuery<Role> {

        #region Public Properties

        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}