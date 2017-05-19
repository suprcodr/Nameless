namespace Nameless.Framework.EventSourcing.Domains {

    internal class AggregateDescriptor {

        #region Internal Properties

        internal AggregateRoot Aggregate { get; set; }
        internal int Version { get; set; }

        #endregion Internal Properties
    }
}