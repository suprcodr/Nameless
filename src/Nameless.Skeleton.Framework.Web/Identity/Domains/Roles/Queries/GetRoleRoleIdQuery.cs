using System;
using Nameless.Skeleton.Framework.Cqrs.Query;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Roles.Queries {

    public class GetRoleRoleIdQuery : IQuery<Guid> {

        #region Public Properties

        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}