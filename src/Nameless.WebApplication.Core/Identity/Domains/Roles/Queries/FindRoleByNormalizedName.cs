using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Queries {

    public sealed class FindRoleByNormalizedNameQuery : IQuery<Role> {

        #region Public Properties

        public string NormalizedName { get; set; }

        #endregion Public Properties
    }

    public sealed class FindRoleByNormalizedNameQueryHandler : QueryHandlerBase<FindRoleByNormalizedNameQuery, Role> {

        #region Public Constructors

        public FindRoleByNormalizedNameQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<Role> HandleAsync(FindRoleByNormalizedNameQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReaderSingle(
                    commandText: EntitySchema.Roles.StoredProcedures.FindRoleByNormalizedName,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Roles.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.NormalizedName, query.NormalizedName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}