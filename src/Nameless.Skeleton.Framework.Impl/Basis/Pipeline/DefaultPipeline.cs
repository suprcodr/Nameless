namespace Nameless.Skeleton.Framework.Basis.Pipeline {

    /// <summary>
    /// Default implementation of the <see cref="IPipeline{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of the data.</typeparam>
    public class DefaultPipeline<T> : IPipeline<T> {

        #region Private Fields

        private IFilter<T> _root;

        #endregion Private Fields

        #region IPipeline<T> Members

        /// <inheritdoc />
        public void Execute(T input) {
            _root?.Execute(input);
        }

        /// <inheritdoc />
        public IPipeline<T> RegisterFilter(IFilter<T> filter) {
            if (_root != null) { _root.RegisterFilter(filter); } else { _root = filter; }

            return this;
        }

        #endregion IPipeline<T> Members
    }
}