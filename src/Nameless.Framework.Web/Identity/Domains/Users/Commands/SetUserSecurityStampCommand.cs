using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserSecurityStampCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string SecurityStamp { get; set; }

        #endregion Public Properties
    }
}