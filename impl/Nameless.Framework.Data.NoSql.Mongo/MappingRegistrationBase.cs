using MongoDB.Bson.Serialization;

namespace Nameless.Framework.Data.NoSql.Mongo {

    /// <summary>
    /// Abstract class to implement BSON class mappings for POCO.
    /// </summary>
    public abstract class MappingRegistrationBase {

        #region Public Abstract Methods

        /// <summary>
        /// Registers a mapping.
        /// </summary>
        /// <returns>The BSON class mapping schema.</returns>
        public abstract BsonClassMap Create();

        #endregion Public Abstract Methods
    }
}