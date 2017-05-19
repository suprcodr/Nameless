using Nameless.Framework.Cqrs.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class CreateUserCommand : ICommand {

        #region Public Properties

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }

        #endregion Public Properties
    }
}