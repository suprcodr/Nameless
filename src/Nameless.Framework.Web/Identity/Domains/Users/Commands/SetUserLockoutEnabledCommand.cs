using System;
using Nameless.Framework.Cqrs.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserLockoutEnabledCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public bool Enabled { get; set; }

        #endregion Public Properties
    }
}