using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserTwoFactorEnabledQuery : IQuery<bool> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserTwoFactorEnabledQueryHandler : QueryHandlerBase<GetUserTwoFactorEnabledQuery, bool> {

        #region Public Constructors

        public GetUserTwoFactorEnabledQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<bool> HandleAsync(GetUserTwoFactorEnabledQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<bool>(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserTwoFactorEnabled,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, query.UserID)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}