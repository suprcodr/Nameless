namespace Nameless.Skeleton.Framework.Cqrs.Command {

    /// <summary>
    /// Command handler interface.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command.</typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand {

        #region Methods

        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="command">The command instance.</param>
        void Handle(TCommand command);

        #endregion Methods
    }
}