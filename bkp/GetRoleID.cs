using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Queries {

    public sealed class GetRoleIDQuery : IQuery<string> {

        #region Public Properties

        public string NormalizedName { get; set; }

        #endregion Public Properties
    }

    public sealed class GetRoleIDQueryHandler : QueryHandlerBase<GetRoleIDQuery, string> {

        #region Public Constructors

        public GetRoleIDQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<string> HandleAsync(GetRoleIDQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<string>(
                    commandText: EntitySchema.Roles.StoredProcedures.GetRoleID,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.NormalizedName, query.NormalizedName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}