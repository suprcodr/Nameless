using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserIDQuery : IQuery<string> {

        #region Public Properties

        public string NormalizedUserName { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserIDQueryHandler : QueryHandlerBase<GetUserIDQuery, string> {

        #region Public Constructors

        public GetUserIDQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<string> HandleAsync(GetUserIDQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<string>(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserID,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.NormalizedUserName, query.NormalizedUserName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}