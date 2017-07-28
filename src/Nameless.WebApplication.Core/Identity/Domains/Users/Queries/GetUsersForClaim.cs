using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUsersForClaimQuery : IQuery<IList<User>> {

        #region Public Properties

        public string ClaimType { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUsersForClaimQueryHandler : QueryHandlerBase<GetUsersForClaimQuery, IList<User>> {

        #region Public Constructors

        public GetUsersForClaimQueryHandler(IApplicationContext appContext, IDatabase database)
            : base(appContext, database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<IList<User>> HandleAsync(GetUsersForClaimQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReader(
                    commandText: EntitySchema.Users.StoredProcedures.GetUsersForClaim,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Users.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.UsersClaims.Fields.Type, query.ClaimType),
                        GetOwnerParameter()
                    }
                );
            }, cancellationToken)
            .ContinueWith(continuation => {
                if (continuation.IsFaulted) {
                    throw continuation.Exception.InnerException;
                }

                if (continuation.IsCanceled) {
                    return ListHelper.Empty<User>();
                }

                return ListHelper.Create(continuation.Result);
            });
        }

        #endregion Public Override Methods
    }
}