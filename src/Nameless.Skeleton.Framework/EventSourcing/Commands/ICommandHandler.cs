using Nameless.Skeleton.Framework.EventSourcing.Messaging;

namespace Nameless.Skeleton.Framework.EventSourcing.Commands {

    /// <summary>
    /// Command handler interface.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command.</typeparam>
    public interface ICommandHandler<in TCommand> : IHandle<TCommand> where TCommand : ICommand { }
}