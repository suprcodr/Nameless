namespace Nameless.Skeleton.Framework.Data {

    /// <summary>
    /// Defines methods for directives.
    /// </summary>
	public interface IDirective {

        #region Methods

        /// <summary>
        /// Executes the directive.
        /// </summary>
        /// <returns>A dynamic representing the directive execution.</returns>
        dynamic Execute(dynamic parameters);

        #endregion Methods
    }
}