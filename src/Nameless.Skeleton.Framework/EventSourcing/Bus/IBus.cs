using Nameless.Skeleton.Framework.EventSourcing.Commands;
using Nameless.Skeleton.Framework.EventSourcing.Events;

namespace Nameless.Skeleton.Framework.EventSourcing.Bus {

    /// <summary>
    /// Defines methods/properties/events to a BUS.
    /// </summary>
    public interface IBus : ICommandSender, IEventPublisher { }
}