using System.Collections.Generic;
using Nameless.Framework.Cqrs.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUsersFromClaimQuery : IQuery<IEnumerable<User>> {

        #region Public Properties

        public UserClaim Claim { get; set; }

        #endregion Public Properties
    }
}