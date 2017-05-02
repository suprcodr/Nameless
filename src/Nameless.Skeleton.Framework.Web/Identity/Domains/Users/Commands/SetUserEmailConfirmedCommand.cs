using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserEmailConfirmedCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public bool Confirmed { get; set; }

        #endregion Public Properties
    }
}