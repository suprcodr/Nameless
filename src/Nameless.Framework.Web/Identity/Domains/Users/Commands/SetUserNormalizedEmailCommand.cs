using System;
using Nameless.Framework.Cqrs.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserNormalizedEmailCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string NormalizedEmail { get; set; }

        #endregion Public Properties
    }
}