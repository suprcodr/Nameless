using System;

namespace Nameless.Skeleton.Framework.ObjectMapper {

    public interface IMapperFunction<TFrom, TTo> {

        #region Methods

        Func<TFrom, TTo> Function { get; }

        #endregion Methods
    }
}