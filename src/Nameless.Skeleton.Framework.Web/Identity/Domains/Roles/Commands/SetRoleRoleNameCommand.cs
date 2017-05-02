using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Commands {

    public class SetRoleRoleNameCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public string Name { get; set; }

        #endregion Public Properties
    }
}