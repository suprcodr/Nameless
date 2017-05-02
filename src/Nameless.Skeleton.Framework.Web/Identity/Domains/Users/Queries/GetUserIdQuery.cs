using System;
using Nameless.Skeleton.Framework.Cqrs.Query;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUserIdQuery : IQuery<Guid> {

        #region Public Properties

        public string NormalizedUserName { get; set; }

        #endregion Public Properties
    }
}