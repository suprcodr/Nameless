using Nameless.Skeleton.Framework.Text;

namespace Nameless.Skeleton.Framework.Network.Email {

    /// <summary>
    /// Interpolator implementation of <see cref="IMessageBodyPreProcessor"/>.
    /// </summary>
    public class InterpolatorMessageBodyPreProcessor : IMessageBodyPreProcessor {

        #region Private Read-Only Fields

        private readonly IInterpolator _interpolator;

        #endregion Private Read-Only Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="InterpolatorMessageBodyPreProcessor"/>.
        /// </summary>
        /// <param name="interpolator">The interpolator instance.</param>
        public InterpolatorMessageBodyPreProcessor(IInterpolator interpolator) {
            Prevent.ParameterNull(interpolator, nameof(interpolator));

            _interpolator = interpolator;
        }

        #endregion Public Constructors

        #region IMessageBodyPreProcessor Members

        /// <inheritdoc />
        public string Process(string body, object data) {
            return _interpolator.Interpolate(body, data);
        }

        #endregion IMessageBodyPreProcessor Members
    }
}