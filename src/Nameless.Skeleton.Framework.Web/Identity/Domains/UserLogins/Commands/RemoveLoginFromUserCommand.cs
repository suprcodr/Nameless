﻿using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Commands {

    public class RemoveLoginFromUserCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string LoginProvider { get; set; }

        #endregion Public Properties
    }
}