using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class FindUserByNormalizedUserNameQuery : IQuery<User> {

        #region Public Properties

        public string NormalizedUserName { get; set; }

        #endregion Public Properties
    }

    public sealed class FindUserByNormalizedUserNameQueryHandler : QueryHandlerBase<FindUserByNormalizedUserNameQuery, User> {

        #region Public Constructors

        public FindUserByNormalizedUserNameQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<User> HandleAsync(FindUserByNormalizedUserNameQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReaderSingle(
                    commandText: EntitySchema.Users.StoredProcedures.FindUserByNormalizedUserName,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Users.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.NormalizedUserName, query.NormalizedUserName)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}