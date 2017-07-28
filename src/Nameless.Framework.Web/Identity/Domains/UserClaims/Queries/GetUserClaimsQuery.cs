using System;
using System.Collections.Generic;
using System.Security.Claims;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.Web.Identity.Domains.UserClaims.Queries {

    public class GetUserClaimsQuery : IQuery<IList<Claim>> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }
}