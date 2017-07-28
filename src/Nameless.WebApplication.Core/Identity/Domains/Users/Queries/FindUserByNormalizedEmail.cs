using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public class FindUserByNormalizedEmailQuery : IQuery<User> {

        #region Public Properties

        public string NormalizedEmail { get; set; }

        #endregion Public Properties
    }

    public sealed class FindUserByNormalizedEmailQueryHandler : QueryHandlerBase<FindUserByNormalizedEmailQuery, User> {

        #region Public Constructors

        public FindUserByNormalizedEmailQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<User> HandleAsync(FindUserByNormalizedEmailQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReaderSingle(
                    commandText: EntitySchema.Users.StoredProcedures.FindUserByNormalizedEmail,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Users.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.NormalizedEmail, query.NormalizedEmail)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}