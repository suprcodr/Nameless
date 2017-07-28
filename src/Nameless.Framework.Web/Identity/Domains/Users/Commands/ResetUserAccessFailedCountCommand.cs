﻿using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Users.Commands {

    public class ResetUserAccessFailedCountCommand : ICommand {

        #region Public Properties

        public Guid UserId { get; set; }

        #endregion Public Properties
    }
}