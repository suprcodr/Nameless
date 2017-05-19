using Nameless.Framework.Cqrs.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Queries {

    public class FindRoleByNameQuery : IQuery<Role> {

        #region Public Properties

        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}