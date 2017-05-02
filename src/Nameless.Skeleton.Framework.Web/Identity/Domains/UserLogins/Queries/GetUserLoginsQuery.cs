using System;
using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Queries {

    public class GetUserLoginsQuery : IQuery<IEnumerable<UserLogin>> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}