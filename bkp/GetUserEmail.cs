﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Query;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Queries {

    public sealed class GetUserEmailQuery : IQuery<string> {

        #region Public Properties

        public Guid UserID { get; set; }

        #endregion Public Properties
    }

    public sealed class GetUserEmailQueryHandler : QueryHandlerBase<GetUserEmailQuery, string> {

        #region Public Constructors

        public GetUserEmailQueryHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task<string> HandleAsync(GetUserEmailQuery query, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.Run(() => {
                return Database.ExecuteScalar<string>(
                    commandText: EntitySchema.Users.StoredProcedures.GetUserEmail,
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