using System;
using Nameless.Framework.CQRS.Query;

namespace Nameless.Framework.Web.Identity.Domains.Roles.Queries {

    public class GetRoleRoleIdQuery : IQuery<Guid> {

        #region Public Properties

        public string NormalizedName { get; set; }

        #endregion Public Properties
    }
}