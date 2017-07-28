using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class FindUserByLoginQuery : IQuery<User> {

        #region Public Properties

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        #endregion Public Properties
    }

    public sealed class FindUserByLoginQueryHandler : QueryHandlerBase<FindUserByLoginQuery, User> {

        #region Public Constructors

        public FindUserByLoginQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<User> HandleAsync(FindUserByLoginQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReaderSingle(
                    commandText: EntitySchema.Users.StoredProcedures.FindUserByLogin,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Users.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.LoginProvider, query.LoginProvider),
                        Parameter.CreateInputParameter(EntitySchema.UsersLogins.Fields.ProviderKey, query.ProviderKey),

                        // TODO: OwnerID
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}