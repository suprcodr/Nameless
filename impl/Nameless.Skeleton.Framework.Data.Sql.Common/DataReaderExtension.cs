using System;
using System.Data;

namespace Nameless.Skeleton.Framework.Data.Sql {

    /// <summary>
    /// Extension methods for <see cref="IDataReader"/>
    /// </summary>
    public static class DataReaderExtension {

        #region Public Static Methods

        public static Guid GetGuidOrDefault(this IDataReader source, string fieldName, Guid fallback = default(Guid)) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (Guid)value : fallback;
        }

        public static string GetStringOrDefault(this IDataReader source, string fieldName, string fallback = null) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (string)value : fallback;
        }

        public static int GetInt32OrDefault(this IDataReader source, string fieldName, int fallback = 0) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (int)value : fallback;
        }

        public static bool GetBooleanOrDefault(this IDataReader source, string fieldName, bool fallback = false) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (bool)value : fallback;
        }

        public static DateTime GetDateTimeOrDefault(this IDataReader source, string fieldName, DateTime fallback) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (DateTime)value : fallback;
        }

        public static DateTime? GetDateTimeOrDefault(this IDataReader source, string fieldName) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (DateTime)value : new DateTime?();
        }

        public static DateTimeOffset GetDateTimeOffsetOrDefault(this IDataReader source, string fieldName, DateTimeOffset fallback) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (DateTimeOffset)value : fallback;
        }

        public static DateTimeOffset? GetDateTimeOffsetOrDefault(this IDataReader source, string fieldName) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (DateTimeOffset)value : new DateTimeOffset?();
        }

        public static byte[] GetBlobOrDefault(this IDataReader source, string fieldName, byte[] fallback = null) {
            Prevent.ParameterNullOrWhiteSpace(fieldName, nameof(fieldName));

            if (source == null) { return fallback; }

            var value = source[fieldName];

            return (value != null && value != DBNull.Value) ? (byte[])value : fallback;
        }

        #endregion Public Static Methods
    }
}