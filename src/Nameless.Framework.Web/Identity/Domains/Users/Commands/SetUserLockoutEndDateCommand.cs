using System;
using Nameless.Framework.Cqrs.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserLockoutEndDateCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public DateTimeOffset? LockoutEndDate { get; set; }

        #endregion Public Properties
    }
}