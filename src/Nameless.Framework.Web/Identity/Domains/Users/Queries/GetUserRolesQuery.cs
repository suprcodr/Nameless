using System;
using System.Collections.Generic;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUserRolesQuery : IQuery<IEnumerable<string>> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}