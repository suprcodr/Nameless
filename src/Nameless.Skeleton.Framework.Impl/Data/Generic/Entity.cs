using System;

namespace Nameless.Skeleton.Framework.Data.Generic {

    public interface IEntity {

        #region Properties

        Guid Id { get; set; }

        #endregion Properties
    }

    public abstract class Entity : IEntity {

        #region Public Properties

        public Guid Id { get; set; }

        #endregion Public Properties
    }
}