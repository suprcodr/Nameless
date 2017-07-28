using System;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUserAccessFailedCountQuery : IQuery<int> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}