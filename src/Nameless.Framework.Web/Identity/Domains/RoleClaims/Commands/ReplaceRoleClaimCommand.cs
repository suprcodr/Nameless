using System;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class ReplaceRoleClaimCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public RoleClaim OldClaim { get; set; }
        public RoleClaim NewClaim { get; set; }

        #endregion Public Properties
    }
}