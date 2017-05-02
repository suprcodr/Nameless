using System;
using System.Collections.Generic;
using Nameless.Skeleton.Framework.EventSourcing.Commands;
using Nameless.Skeleton.Framework.EventSourcing.Events;
using Nameless.Skeleton.Framework.IoC;

namespace Nameless.Skeleton.Framework.EventSourcing.Bus {

    public class DefaultBus : IBus {

        #region Private Read-Only Fields

        private readonly IResolver _resolver;

        #endregion Private Read-Only Fields

        #region Public Constructors

        public DefaultBus(IResolver resolver) {
            Prevent.ParameterNull(resolver, nameof(resolver));

            _resolver = resolver;
        }

        #endregion Public Constructors

        #region IBus Members

        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent {
            var handlers = _resolver.Resolve<IEnumerable<IEventHandler<TEvent>>>();
            foreach (var handler in handlers) {
                handler.Handle(evt);
            }
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand {
            var handler = _resolver.Resolve<ICommandHandler<TCommand>>();

            if (handler == null) {
                throw new InvalidOperationException();
            }

            handler.Handle(command);
        }

        #endregion IBus Members
    }
}