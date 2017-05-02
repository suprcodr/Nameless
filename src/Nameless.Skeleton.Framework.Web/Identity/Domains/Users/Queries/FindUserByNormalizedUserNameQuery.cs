using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class FindUserByNormalizedUserNameQuery : IQuery<User> {

        #region Public Properties

        public string NormalizedUserName { get; set; }

        #endregion Public Properties
    }
}