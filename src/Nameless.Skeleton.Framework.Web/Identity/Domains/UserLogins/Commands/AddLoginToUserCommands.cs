﻿using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Commands {

    public class AddLoginToUserCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        #endregion Public Properties
    }
}