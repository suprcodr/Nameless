using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Generic;
using Nameless.Framework.Web.Identity.Models;

namespace Nameless.Framework.Web.Identity.Domains.Users.Queries {

    public class FindUserByEmailQuery : IQuery<User> {

        #region Public Properties

        public string NormalizedEmail { get; set; }

        #endregion Public Properties
    }

    public class FindUserByEmailQueryHandler : QueryHandlerBase<FindUserByEmailQuery, User> {

        #region Public Constructors

        public FindUserByEmailQueryHandler(IRepository repository)
            : base(repository) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override User Handle(FindUserByEmailQuery query) {
            return FindOne<User>(_ => _.NormalizedEmail == query.NormalizedEmail);
        }

        #endregion Public Override Methods
    }
}