using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserPhoneNumberConfirmedQuery : IQuery<bool> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserPhoneNumberConfirmedQueryHandler : QueryHandlerBase<GetUserPhoneNumberConfirmedQuery, bool> {

        #region Public Constructors

        public GetUserPhoneNumberConfirmedQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<bool> HandleAsync(GetUserPhoneNumberConfirmedQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<bool>(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserPhoneNumberConfirmed,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, query.UserID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}