using Nameless.Framework.EventSourcing.Messaging;

namespace Nameless.Framework.EventSourcing.Commands {

    /// <summary>
    /// Defines methods/properties/events to a command.
    /// </summary>
    public interface ICommand : IMessage {

        #region Properties

        /// <summary>
        /// Gets the expected version of the command.
        /// </summary>
        int ExpectedVersion { get; }

        #endregion Properties
    }
}