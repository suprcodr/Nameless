using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class FindUserByEmailQuery : IQuery<User> {

        #region Public Properties

        public string NormalizedEmail { get; set; }

        #endregion Public Properties
    }
}