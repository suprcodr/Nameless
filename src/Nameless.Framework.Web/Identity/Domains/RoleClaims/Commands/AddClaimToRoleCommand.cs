using System;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class AddClaimToRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public RoleClaim Claim { get; set; }

        #endregion Public Properties
    }
}