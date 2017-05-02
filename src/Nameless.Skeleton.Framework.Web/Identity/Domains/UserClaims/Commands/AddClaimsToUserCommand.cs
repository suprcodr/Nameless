using System;
using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Command;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Commands {

    public class AddClaimsToUserCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }

        #endregion Public Properties
    }
}