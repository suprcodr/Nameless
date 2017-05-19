namespace Nameless.Framework.Cqrs.Command {

    /// <summary>
    /// Command dispatcher interface.
    /// </summary>
    public interface ICommandDispatcher {

        #region Methods

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="command">The instance of the command.</param>
        void Command<TCommand>(TCommand command)
            where TCommand : ICommand;

        #endregion Methods
    }
}