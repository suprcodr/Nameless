using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Roles.Queries {

    public sealed class FindRoleByIDQuery : IQuery<Role> {

        #region Public Properties

        public string RoleID { get; set; }

        #endregion Public Properties
    }

    public sealed class FindRoleByIDQueryHandler : QueryHandlerBase<FindRoleByIDQuery, Role> {

        #region Public Constructors

        public FindRoleByIDQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<Role> HandleAsync(FindRoleByIDQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReaderSingle(
                    commandText: EntitySchema.Roles.StoredProcedures.FindRoleByID,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Roles.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Roles.Fields.ID, query.RoleID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}