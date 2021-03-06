﻿using System;
using Nameless.Framework.CQRS.Command;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Commands {

    public class UpdateRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}