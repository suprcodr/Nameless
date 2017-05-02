using System;

namespace Nameless.Skeleton.Framework.ObjectMapper {

    /// <summary>
    /// Defines methods/properties to project an <see cref="object"/> from one type to another.
    /// </summary>
	public interface IMapper {

        #region Methods

        /// <summary>
        /// Projects an <see cref="object"/> to another type.
        /// </summary>
        /// <param name="instance">The instance of the <see cref="object"/>.</param>
        /// <param name="from">The origin type of the object.</param>
        /// <param name="to">The destination type of the object.</param>
        /// <returns>An instance of the destination type.</returns>
        object Map(object instance, Type from, Type to);

        #endregion Methods
    }
}