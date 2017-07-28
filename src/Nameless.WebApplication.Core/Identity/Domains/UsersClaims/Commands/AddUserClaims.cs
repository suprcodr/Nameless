using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.UsersClaims.Commands {

    public sealed class AddUserClaimsCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public IDictionary<string, string> Claims { get; set; }

        #endregion Public Properties
    }

    public sealed class AddUserClaimsCommandHandler : CommandHandlerBase<AddUserClaimsCommand> {

        #region Public Constructors

        public AddUserClaimsCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(AddUserClaimsCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                var counter = 0;
                command.Claims.Each(kvp => {
                    progress.Report(++counter);
                    cancellationToken.ThrowIfCancellationRequested();

                    Database.ExecuteNonQuery(
                        commandText: EntitySchema.UsersClaims.StoredProcedures.AddUserClaim,
                        commandType: CommandType.StoredProcedure,
                        parameters: new[] {
                            Parameter.CreateInputParameter(EntitySchema.UsersClaims.Fields.UserID, command.UserID, DbType.Guid),
                            Parameter.CreateInputParameter(EntitySchema.UsersClaims.Fields.Type, kvp.Key),
                            Parameter.CreateInputParameter(EntitySchema.UsersClaims.Fields.Value, kvp.Value)
                        }
                    );
                });
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}