using System;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

    public class FindUserByIdQuery : IQuery<User> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}