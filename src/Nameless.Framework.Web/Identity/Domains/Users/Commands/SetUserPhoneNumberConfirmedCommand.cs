using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserPhoneNumberConfirmedCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public bool Confirmed { get; set; }

        #endregion Public Properties
    }
}