using System;
using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Queries {

    public class GetUserClaimsQuery : IQuery<IEnumerable<UserClaim>> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}