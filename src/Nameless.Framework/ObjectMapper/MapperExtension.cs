namespace Nameless.Framework.ObjectMapper {

    /// <summary>
    /// Extension methods for <see cref="IMapper"/>.
    /// </summary>
    public static class MapperExtension {

        #region Public Static Methods

        /// <summary>
        /// Generic implementation of <see cref="IMapper.Map(object, System.Type, System.Type)"/>.
        /// </summary>
        /// <typeparam name="TTo">Type of TO</typeparam>
        /// <param name="source">The source <see cref="IMapper"/>.</param>
        /// <param name="instance">The instance to map.</param>
        /// <returns>An instance of <typeparamref name="TTo"/> .</returns>
        public static TTo Map<TTo>(this IMapper source, object instance)
            where TTo : class {
            Prevent.ParameterNull(instance, nameof(instance));

            if (source == null) { return default(TTo); }

            return (TTo)source.Map(instance, instance.GetType(), typeof(TTo));
        }

        #endregion Public Static Methods
    }
}