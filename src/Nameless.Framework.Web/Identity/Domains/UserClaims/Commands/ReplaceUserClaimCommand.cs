using System;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.UserClaims.Commands {

    public class ReplaceUserClaimCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public UserClaim OldClaim { get; set; }
        public UserClaim NewClaim { get; set; }

        #endregion Public Properties
    }
}