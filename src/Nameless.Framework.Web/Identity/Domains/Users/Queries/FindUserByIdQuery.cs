using System;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class FindUserByIdQuery : IQuery<User> {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}