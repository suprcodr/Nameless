using Nameless.Framework.Cqrs.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.UserLogins.Queries {

    public class FindUserByLoginQuery : IQuery<User> {

        #region Public Properties

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        #endregion Public Properties
    }
}