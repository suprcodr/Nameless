using System;
using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Command;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.RoleClaims.Commands {

    public class RemoveRoleClaimsCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public IEnumerable<RoleClaim> Claims { get; set; }

        #endregion Public Properties
    }
}