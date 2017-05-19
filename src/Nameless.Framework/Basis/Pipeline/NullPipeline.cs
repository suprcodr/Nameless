namespace Nameless.Framework.Basis.Pipeline {

    /// <summary>
    /// Null Object Pattern implementation for <see cref="IPipeline{T}"/>.
    /// </summary>
    /// <remarks>https://en.wikipedia.org/wiki/Null_Object_pattern</remarks>
    public sealed class NullPipeline<T> : IPipeline<T> {

        #region Public Static Fields

        /// <summary>
        /// Gets the static current instance of <see cref="NullPipeline{T}"/>.
        /// </summary>
        public static readonly IPipeline<T> Instance = new NullPipeline<T>();

        #endregion Public Static Fields

        #region Private Constructors

        // Block construction of NullLogger
        private NullPipeline() { }

        #endregion Private Constructors

        #region IPipeline<T> Members

        /// <inheritdoc />
        public void Execute(T input) { }

        /// <inheritdoc />
        public IPipeline<T> RegisterFilter(IFilter<T> filter) { return this; }

        #endregion IPipeline<T> Members
    }
}