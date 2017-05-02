using System;
using Nameless.Skeleton.Framework.Cqrs.Command;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Commands {

    public class ReplaceUserClaimCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public UserClaim OldClaim { get; set; }
        public UserClaim NewClaim { get; set; }

        #endregion Public Properties
    }
}