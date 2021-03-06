﻿using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Commands {

    public class SetRoleNormalizedRoleNameCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}