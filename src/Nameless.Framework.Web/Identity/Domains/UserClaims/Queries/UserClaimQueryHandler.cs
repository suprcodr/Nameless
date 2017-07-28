using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Generic;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.UserClaims.Queries {

    public class UserClaimQueryHandler : IQueryHandler<GetUserClaimsQuery, IList<Claim>> {

        #region Private Read-Only Fields

        private readonly IRepository _repository;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserClaimQueryHandler(IRepository repository) {
            Prevent.ParameterNull(repository, nameof(repository));

            _repository = repository;
        }

        #endregion Public Constructors

        #region IQueryHandler<ListUserClaimsQuery, IEnumerable<UserClaim>> Members

        public Task<IList<Claim>> HandleAsync(GetUserClaimsQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                IList<Claim> claims = _repository
                    .FindAll<UserClaim>(_ => _.User.ID == query.UserID)
                    .Select(UserClaim.ToClaim)
                    .ToList();

                return claims;
            });
        }

        #endregion IQueryHandler<ListUserClaimsQuery, IEnumerable<UserClaim>> Members
    }
}