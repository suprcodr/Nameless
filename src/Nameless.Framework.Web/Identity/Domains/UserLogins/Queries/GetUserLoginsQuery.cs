using System;
using System.Collections.Generic;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.UserLogins.Queries {

    public class GetUserLoginsQuery : IQuery<IEnumerable<UserLogin>> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}