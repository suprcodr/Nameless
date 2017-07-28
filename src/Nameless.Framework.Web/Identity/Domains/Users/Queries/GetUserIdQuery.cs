using System;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUserIdQuery : IQuery<Guid> {

        #region Public Properties

        public string NormalizedUserName { get; set; }

        #endregion Public Properties
    }
}