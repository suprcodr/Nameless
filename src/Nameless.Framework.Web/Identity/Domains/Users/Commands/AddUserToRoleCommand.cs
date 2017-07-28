using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class AddUserToRoleCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string RoleName { get; set; }

        #endregion Public Properties
    }
}