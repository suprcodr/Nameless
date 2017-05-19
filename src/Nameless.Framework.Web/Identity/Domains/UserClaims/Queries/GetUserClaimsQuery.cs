using System;
using System.Collections.Generic;
using Nameless.Framework.Cqrs.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.UserClaims.Queries {

    public class GetUserClaimsQuery : IQuery<IEnumerable<UserClaim>> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}