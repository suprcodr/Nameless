using System;
using Nameless.Framework.Cqrs.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class IncrementUserAccessFailedCountCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}