﻿using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class SetUserUserNameCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }
        public string UserName { get; set; }

        #endregion Public Properties
    }
}