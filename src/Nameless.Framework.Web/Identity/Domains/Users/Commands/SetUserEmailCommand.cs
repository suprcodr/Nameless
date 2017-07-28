using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserEmailCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string Email { get; set; }

        #endregion Public Properties
    }
}