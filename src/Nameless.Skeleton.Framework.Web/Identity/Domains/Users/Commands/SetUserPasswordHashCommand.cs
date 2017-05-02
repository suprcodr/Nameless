using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserPasswordHashCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string PasswordHash { get; set; }

        #endregion Public Properties
    }
}