using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserRolesQuery : IQuery<IList<string>> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserRolesQueryHandler : QueryHandlerBase<GetUserRolesQuery, IList<string>> {

        #region Public Constructors

        public GetUserRolesQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<IList<string>> HandleAsync(GetUserRolesQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReader(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserRoles,
                    commandType: CommandType.StoredProcedure,
                    mapper: (reader) => reader.GetStringOrDefault(EntitySchema.Roles.Fields.Name),
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
                    return ListHelper.Empty<string>();
                }

                return ListHelper.Create(continuation.Result);
            });
        }

        #endregion Public Override Methods
    }
}