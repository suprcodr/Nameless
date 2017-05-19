using System;

namespace Nameless.Framework.Network.PubSub {

    public interface ISubscription<TMessage> {

        #region Methods

        Action<TMessage> CreateHandler();

        #endregion Methods
    }
}