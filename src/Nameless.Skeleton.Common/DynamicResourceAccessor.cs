using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Nameless.Skeleton {

    /// <summary>
    /// Gives access, dynamically, to the resources inside an assembly.
    /// </summary>
    public abstract class DynamicResourceAccessor : DynamicObject {

        #region Private Read-Only Fields

        private readonly IResourceManager _resourceManager;
        private readonly string _resourceNamePattern;

        #endregion Private Read-Only Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DynamicResourceAccessor"/>.
        /// </summary>
        /// <param name="resourceAssembly">The resource assembly.</param>
        /// <param name="resourceNamePattern">A pattern to match the resource name inside the resource assembly.</param>
        protected DynamicResourceAccessor(Assembly resourceAssembly, string resourceNamePattern = null) {
            Prevent.ParameterNull(resourceAssembly, nameof(resourceAssembly));

            _resourceManager = new ResourceManager(resourceAssembly);
            _resourceNamePattern = resourceNamePattern;
        }

        #endregion Protected Constructors

        #region Public Override Methods

        /// <inheritdoc />
        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            result = null;

            var filter = _resourceNamePattern != null
                ? string.Format(_resourceNamePattern, binder.Name)
                : binder.Name;

            var resourceName = _resourceManager.GetResourceNames(filter).SingleOrDefault();
            if (resourceName != null) {
                result = _resourceManager.GetString(resourceName);
            }
            return (resourceName != null);
        }

        #endregion Public Override Methods
    }
}