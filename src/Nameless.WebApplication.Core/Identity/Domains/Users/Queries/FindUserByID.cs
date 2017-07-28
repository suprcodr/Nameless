using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;
using Nameless.WebApplication.Core.Identity.Models;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class FindUserByIDQuery : IQuery<User> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class FindUserByIDQueryHandler : QueryHandlerBase<FindUserByIDQuery, User> {

        #region Public Constructors

        public FindUserByIDQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<User> HandleAsync(FindUserByIDQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteReaderSingle(
                    commandText: EntitySchema.Users.StoredProcedures.FindUserByID,
                    commandType: CommandType.StoredProcedure,
                    mapper: EntitySchema.Users.Mapper,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, query.UserID, DbType.Guid)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}