using System.Collections.Generic;
using System.Data;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Resources;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserClaims.Queries {

    public class UserClaimQueryHandler : IQueryHandler<GetUserClaimsQuery, IEnumerable<UserClaim>> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserClaimQueryHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region IQueryHandler<ListUserClaimsQuery, IEnumerable<UserClaim>> Members

        public IEnumerable<UserClaim> Handle(GetUserClaimsQuery query) {
            return _database.ExecuteReader((string)SQL.Instance.GetUserClaims, Mappers.GetUserClaims, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId, DbType.Guid)
            });
        }

        #endregion IQueryHandler<ListUserClaimsQuery, IEnumerable<UserClaim>> Members
    }
}