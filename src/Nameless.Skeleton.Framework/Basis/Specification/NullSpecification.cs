namespace Nameless.Skeleton.Framework.Basis.Specification {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="ISpecification{T}"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullSpecification<T> : ISpecification<T> where T : class {

        #region Public Static Fields

        /// <summary>
        /// Gets the static current instance of <see cref="NullSpecification{T}"/>.
        /// </summary>
        public static readonly ISpecification<T> Instance = new NullSpecification<T>();

        #endregion Public Static Fields

        #region Private Constructors

        // Block construction of NullLogger
        private NullSpecification() { }

        #endregion Private Constructors

        #region ISpecification<T> Members

        /// <inheritdoc />
        public bool IsSatisfiedBy(T candidate) { return false; }

        /// <inheritdoc />
		public ISpecification<T> And(ISpecification<T> other) { return this; }

        /// <inheritdoc />
        public ISpecification<T> Or(ISpecification<T> other) { return this; }

        /// <inheritdoc />
		public ISpecification<T> Not() { return this; }

        #endregion ISpecification<T> Members
    }
}