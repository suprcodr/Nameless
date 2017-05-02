using System;
using Nameless.Skeleton.Framework.Cqrs.Query;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class GetUserEmailConfirmedQuery : IQuery<bool> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}