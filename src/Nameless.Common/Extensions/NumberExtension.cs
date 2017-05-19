using System;

namespace Nameless {

    /// <summary>
    /// Expression methods for number value type.
    /// </summary>
	public static class NumberExtension {

        #region Public Static Methods

        /// <summary>
        /// Runs a loop by the number of times that the <paramref name="source"/> parameter informs.
        /// Using the action specified.
        /// </summary>
        /// <param name="source">The source <see cref="int"/>.</param>
        /// <param name="action">The action of the interaction.</param>
        public static void Times(this int source, Action action) {
            Times(source, _ => action());
        }

        /// <summary>
        /// Runs a loop by the number of times that the <paramref name="source"/> parameter informs.
        /// Using the action specified. In this case, the action receives a parameter, that will be
        /// the index of the interaction.
        /// </summary>
        /// <param name="source">The source <see cref="int"/>.</param>
        /// <param name="action">The action of the interaction.</param>
		public static void Times(this int source, Action<int> action) {
            for (var idx = 0; idx < source; idx++) {
                action(idx);
            }
        }

        #endregion Public Static Methods
    }
}