using System.Collections.Generic;
using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data.Ado;
using Nameless.Skeleton.Framework.Web.Identity.Domains.Resources;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.UserLogins.Queries {

    public class UserLoginQueryHandler : IQueryHandler<FindUserByLoginQuery, User>,
                                         IQueryHandler<GetUserLoginsQuery, IEnumerable<UserLogin>> {

        #region Private Read-Only Fields

        private readonly IDatabase _database;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public UserLoginQueryHandler(IDatabase database) {
            Prevent.ParameterNull(database, nameof(database));

            _database = database;
        }

        #endregion Public Constructors

        #region IQueryHandler<FindUserByLoginQuery, User> Members

        public User Handle(FindUserByLoginQuery query) {
            return _database.ExecuteReaderSingle((string)SQL.Instance.FindUserByLogin, Mappers.FindUserByLogin, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.LoginProvider), query.LoginProvider),
                Parameter.CreateInputParameter(nameof(query.ProviderKey), query.ProviderKey)
            });
        }

        #endregion IQueryHandler<FindUserByLoginQuery, User> Members

        #region IQueryHandler<GetUserLoginsQuery, IEnumerable<UserLogin>> Members

        public IEnumerable<UserLogin> Handle(GetUserLoginsQuery query) {
            return _database.ExecuteReader((string)SQL.Instance.GetUserLogins, Mappers.GetUserLogins, parameters: new[] {
                Parameter.CreateInputParameter(nameof(query.UserId), query.UserId)
            });
        }

        #endregion IQueryHandler<GetUserLoginsQuery, IEnumerable<UserLogin>> Members
    }
}