﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Nameless.Framework.CQRS.Command;
using Nameless.Framework.Data.Ado;

namespace Nameless.WebApplication.Core.Identity.Domains.Users.Commands {

    public sealed class SetUserPhoneNumberConfirmedCommand : ICommand {

        #region Public Properties

        public Guid UserID { get; set; }
        public bool Confirmed { get; set; }

        #endregion Public Properties
    }

    public sealed class SetUserPhoneNumberConfirmedCommandHandler : CommandHandlerBase<SetUserPhoneNumberConfirmedCommand> {

        #region Public Constructors

        public SetUserPhoneNumberConfirmedCommandHandler(IDatabase database)
            : base(database) { }

        #endregion Public Constructors

        #region Public Override Methods

        public override Task HandleAsync(SetUserPhoneNumberConfirmedCommand command, CancellationToken cancellationToken = default(CancellationToken), IProgress<int> progress = null) {
            return Task.Run(() => {
                Database.ExecuteNonQuery(
                    commandText: EntitySchema.Users.StoredProcedures.SetUserPhoneNumberConfirmed,
                    commandType: CommandType.StoredProcedure,
                    parameters: new[] {
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.ID, command.UserID, DbType.Guid),
                        Parameter.CreateInputParameter(EntitySchema.Users.Fields.PhoneNumberConfirmed, command.Confirmed)
                    }
                );
            }, cancellationToken);
        }

        #endregion Public Override Methods
    }
}