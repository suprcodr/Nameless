using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Generic;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.RoleClaims.Queries {

    public class RoleClaimQueryHandler : IQueryHandler<GetRoleClaimsQuery, IEnumerable<RoleClaim>> {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public RoleClaimQueryHandler(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region IQueryHandler<ListRoleClaimsQuery, IEnumerable<RoleClaim>> Members

        public Task<IEnumerable<RoleClaim>> HandleAsync(GetRoleClaimsQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return _repository.Query<RoleClaim>().AsEnumerable();
            });
        }

        #endregion IQueryHandler<ListRoleClaimsQuery, IEnumerable<RoleClaim>> Members
    }
}