﻿using Nameless.Skeleton.Framework.Cqrs.Query;
using Nameless.Skeleton.Framework.Data;
using Nameless.Skeleton.Framework.Web.Identity.Models;

namespace Nameless.Skeleton.Framework.Web.Identity.Domains.Users.Queries {

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