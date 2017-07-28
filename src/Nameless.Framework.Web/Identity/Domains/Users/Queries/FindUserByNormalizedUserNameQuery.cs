using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class FindUserByNormalizedUserNameQuery : IQuery<User> {

        #region Public Properties

        public string NormalizedUserName { get; set; }

        #endregion Public Properties
    }
}