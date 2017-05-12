﻿namespace Nameless.Skeleton.Framework.Cqrs.Command {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="ICommandDispatcher"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullCommandDispatcher : ICommandDispatcher {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="NullCommandDispatcher"/>.
        /// </summary>
        public static readonly ICommandDispatcher Instance = new NullCommandDispatcher();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullCommandDispatcher() {
        }

        #endregion Private Constructors

        #region ICommandDispatcher Members

        /// <inheritdoc />
        public void Command<TCommand>(TCommand command) where TCommand : ICommand {
        }

        #endregion ICommandDispatcher Members
    }
}