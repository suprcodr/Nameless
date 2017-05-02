using Nameless.Skeleton.Framework.EventSourcing.Commands;
using Nameless.Skeleton.Framework.EventSourcing.Events;

namespace Nameless.Skeleton.Framework.EventSourcing.Bus {

    /// <summary>
    /// Null Object Pattern implementation of <see cref="IBus"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullBus : IBus {

        #region Public Static Read-Only Fields

        /// <summary>
        /// Gets the static instance of <see cref="IBus"/>.
        /// </summary>
        public static readonly IBus Instance = new NullBus();

        #endregion Public Static Read-Only Fields

        #region Private Constructors

        private NullBus() {
        }

        #endregion Private Constructors

        #region IBus Members

        /// <inheritdoc />
        ///
        public void Send<TCommand>(TCommand command) where TCommand : ICommand {
        }

        /// <inheritdoc />
        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent {
        }

        #endregion IBus Members
    }
}