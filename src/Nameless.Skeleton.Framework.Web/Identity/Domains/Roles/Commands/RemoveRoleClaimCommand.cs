﻿using System;
using Nameless.Skeleton.Framework.Cqrs.Command;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Commands {

    public class RemoveRoleClaimCommand : ICommand {

        #region Public Properties

        public Guid RoleId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        #endregion Public Properties
    }
}