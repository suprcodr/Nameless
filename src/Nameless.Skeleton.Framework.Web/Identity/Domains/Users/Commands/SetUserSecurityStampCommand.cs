using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserSecurityStampCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string SecurityStamp { get; set; }

        #endregion Public Properties
    }
}