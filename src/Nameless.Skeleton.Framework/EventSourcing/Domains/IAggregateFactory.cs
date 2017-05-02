namespace Nameless.Skeleton.Framework.EventSourcing.Domains {

    /// <summary>
    /// Defines methods/properties/events to create an aggregate factory.
    /// </summary>
    public interface IAggregateFactory {

        #region Methods

        /// <summary>
        /// Creates an aggregate root instance.
        /// </summary>
        /// <typeparam name="TAggregate">Type of the aggregate root.</typeparam>
        /// <returns>The aggregate root instance.</returns>
        TAggregate Create<TAggregate>() where TAggregate : AggregateRoot;

        #endregion Methods
    }
}