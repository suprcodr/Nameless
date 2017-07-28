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

namespace Nameless.WebApplication.Core.Identity.Domains.RolesClaims.Queries {

    public sealed class GetRoleClaimsQuery : IQuery<IList<Claim>> {

        #region Public Properties

        public Guid RoleID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetRoleClaimsQueryHandler : QueryHandlerBase<GetRoleClaimsQuery, IList<Claim>> {

        #region Public Constructors

        public GetRoleClaimsQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<IList<Claim>> HandleAsync(GetRoleClaimsQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReader(
                    commandText: EntitySchema.RolesClaims.StoredProcedures.GetRoleClaims,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.RolesClaims.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.RolesClaims.Fields.RoleID, query.RoleID, DbType.Guid)
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

                return ListHelper.Create(continuation.Result.Select(RoleClaim.ToClaim));
            });
        }

        #endregion Public Override Methods
    }
}