using System;
using Nameless.Framework.Cqrs.Command;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Commands {

    public class DeleteRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }

        #endregion Public Properties
    }
}