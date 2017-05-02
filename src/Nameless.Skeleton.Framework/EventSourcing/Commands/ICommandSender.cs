namespace Nameless.Skeleton.Framework.EventSourcing.Commands {

    /// <summary>
    /// Command dispatcher interface.
    /// </summary>
    public interface ICommandSender {

        #region Methods

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <typeparam name="TCommand">Type of the command.</typeparam>
        /// <param name="command">The instance of the command.</param>
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;

        #endregion Methods
    }
}