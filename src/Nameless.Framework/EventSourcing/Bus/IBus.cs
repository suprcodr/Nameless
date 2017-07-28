using Nameless.Framework.EventSourcing.Commands;
using Nameless.Framework.EventSourcing.Events;

namespace Nameless.Framework.EventSourcing.Bus {

    /// <summary>
    /// Interface for BUS implementation.
    /// </summary>
    public interface IBus : ICommandDispatcher, IEventPublisher { }
}