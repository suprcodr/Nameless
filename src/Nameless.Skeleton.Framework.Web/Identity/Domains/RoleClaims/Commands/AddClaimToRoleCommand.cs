using System;
using Nameless.Skeleton.Framework.Cqrs.Command;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class AddClaimToRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public RoleClaim Claim { get; set; }

        #endregion Public Properties
    }
}