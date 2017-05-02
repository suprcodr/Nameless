﻿using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Commands {

    public class CreateRoleCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}