using Nameless.Framework.EventSourcing.Commands;
using Nameless.Framework.EventSourcing.Events;

namespace Nameless.Framework.EventSourcing.Bus {

    /// <summary>
    /// Defines methods/properties/events to a BUS.
    /// </summary>
    public interface IBus : ICommandSender, IEventPublisher { }
}