using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.UsersClaims.Queries {

    public sealed class GetUserClaimsQuery : IQuery<IList<Claim>> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserClaimsQueryHandler : QueryHandlerBase<GetUserClaimsQuery, IList<Claim>> {

        #region Public Constructors

        public GetUserClaimsQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<IList<Claim>> HandleAsync(GetUserClaimsQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReader(
                    commandText: EntitySchema.UsersClaims.StoredProcedures.GetUserClaims,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.UsersClaims.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.UsersClaims.Fields.UserID, query.UserID, DbType.Guid)
                    }
                );
            }, cancellationToken)
            .ContinueWith(continuation => {
                if (continuation.IsFaulted) {
                    throw continuation.Exception.InnerException;
                }

                if (continuation.IsCanceled) {
                    return ListHelper.Empty<Claim>();
                }

                return ListHelper.Create(continuation.Result.Select(UserClaim.ToClaim));
            });
        }

        #endregion Public Override Methods
    }
}