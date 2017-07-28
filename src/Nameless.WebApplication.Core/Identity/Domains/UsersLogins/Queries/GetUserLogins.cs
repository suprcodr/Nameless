using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.UsersLogins.Queries {

    public sealed class GetUserLoginsQuery : IQuery<IList<UserLoginInfo>> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserLoginsQueryHandler : QueryHandlerBase<GetUserLoginsQuery, IList<UserLoginInfo>> {

        #region Public Constructors

        public GetUserLoginsQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<IList<UserLoginInfo>> HandleAsync(GetUserLoginsQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReader(
                    commandText: EntitySchema.UsersLogins.StoredProcedures.GetUserLogins,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.UsersLogins.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, query.UserID, DbType.Guid)
                    }
                );
            }, cancellationToken)
            .ContinueWith(continuation => {
                if (continuation.IsFaulted) {
                    throw continuation.Exception.InnerException;
                }

                if (continuation.IsCanceled) {
                    return ListHelper.Empty<UserLoginInfo>();
                }

                return ListHelper.Create(continuation.Result.Select(UserLogin.ToUserLoginInfo));
            });
        }

        #endregion Public Override Methods
    }
}