using System;

namespace Nameless {

    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtension {

        #region Public Static Methods

        /// <summary>
        /// Gets the difference, in years, between the <paramref name="source"/> <see cref="DateTime"/> and <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="source">The source <see cref="DateTime"/>.</param>
        /// <returns>An integer representation of the difference.</returns>
        public static int GetYearsToToday(this DateTime source) {
            return GetYears(source, DateTime.Today);
        }

        /// <summary>
        /// Gets the difference, in years, between the <paramref name="source"/> <see cref="DateTime"/> and the <paramref name="future"/> <see cref="DateTime"/>.
        /// </summary>
        /// <param name="source">The source <see cref="DateTime"/>.</param>
        /// <param name="future">The future <see cref="DateTime"/>.</param>
        /// <returns>An integer representation of the difference.</returns>
        public static int GetYears(this DateTime source, DateTime future) {
            var years = future.Year - source.Year;

            if (future.Month < source.Month) { --years; }
            if (future.Month == source.Month && future.Day < source.Day) { --years; }

            return years;
        }

        #endregion Public Static Methods
    }
}