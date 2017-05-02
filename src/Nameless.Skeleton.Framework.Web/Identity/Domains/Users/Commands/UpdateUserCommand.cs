using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Commands {

    public class UpdateUserCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }

        #endregion Public Properties
    }
}