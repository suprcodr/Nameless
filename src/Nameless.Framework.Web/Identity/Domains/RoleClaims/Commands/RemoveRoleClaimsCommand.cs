using System;
using System.Collections.Generic;
using Nameless.Framework.Cqrs.Command;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class RemoveRoleClaimsCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public IEnumerable<RoleClaim> Claims { get; set; }

        #endregion Public Properties
    }
}