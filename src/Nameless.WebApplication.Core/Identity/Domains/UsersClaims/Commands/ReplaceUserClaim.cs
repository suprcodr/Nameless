using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.UsersClaims.Commands {

    public sealed class ReplaceUserClaimCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }

        public string OldClaimType { get; set; }

        public string NewClaimType { get; set; }
        public string NewClaimValue { get; set; }

        #endregion Public Properties
    }

    public sealed class ReplaceUserClaimCommandHandler : CommandHandlerBase<ReplaceUserClaimCommand> {

        #region Public Constructors

        public ReplaceUserClaimCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(ReplaceUserClaimCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.UsersClaims.StoredProcedures.ReplaceUserClaim,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.UsersClaims.Fields.UserID, command.UserID, DbType.Guid),

                        Parameter.CreateInputParameter(string.Concat("old_", EntitySchema.UsersClaims.Fields.Type), command.OldClaimType),
                        
                        Parameter.CreateInputParameter(string.Concat("new_", EntitySchema.UsersClaims.Fields.Type), command.NewClaimType),
                        Parameter.CreateInputParameter(string.Concat("new_", EntitySchema.UsersClaims.Fields.Value), command.NewClaimValue)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}