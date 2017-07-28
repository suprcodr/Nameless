using System;
using System.Collections.Generic;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.UserClaims.Commands {

    public class RemoveUserClaimsCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }

        #endregion Public Properties
    }
}