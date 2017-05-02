using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Skeleton.Framework.Data.Ado {

    /// <summary>
    /// Extension methods for <see cref="IDatabase"/>
    /// </summary>
    public static class DatabaseExtension {

        #region Public Static Methods

        public static Task ExecuteNonQueryAsync(this IDatabase source, string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            return ExecuteNonQueryAsync(source, commandText, CancellationToken.None, commandType, parameters);
        }

        public static Task ExecuteNonQueryAsync(this IDatabase source, string commandText, CancellationToken cancellationToken, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            if (source == null) { return Task.CompletedTask; }

            return Task.Run(() => source.ExecuteNonQuery(commandText, commandType, parameters), cancellationToken);
        }

        public static Task<object> ExecuteScalarAsync(this IDatabase source, string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            return ExecuteScalarAsync(source, commandText, CancellationToken.None, commandType, parameters);
        }

        public static Task<object> ExecuteScalarAsync(this IDatabase source, string commandText, CancellationToken cancellationToken, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            if (source == null) { return Task.FromResult<object>(null); }

            return Task.Run(() => source.ExecuteScalar(commandText, commandType, parameters), cancellationToken);
        }

        public static Task<IEnumerable<TResult>> ExecuteReaderAsync<TResult>(this IDatabase source, string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            return ExecuteReaderAsync(source, commandText, CancellationToken.None, mapper, commandType, parameters);
        }

        public static Task<IEnumerable<TResult>> ExecuteReaderAsync<TResult>(this IDatabase source, string commandText, CancellationToken cancellationToken, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            if (source == null) { return Task.FromResult(Enumerable.Empty<TResult>()); }

            return Task.Run(() => source.ExecuteReader(commandText, mapper, commandType, parameters), cancellationToken);
        }

        public static Task<TResult> ExecuteReaderSingleAsync<TResult>(this IDatabase source, string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            return ExecuteReaderSingleAsync(source, commandText, CancellationToken.None, mapper, commandType, parameters);
        }

        public static Task<TResult> ExecuteReaderSingleAsync<TResult>(this IDatabase source, string commandText, CancellationToken cancellationToken, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            if (source == null) { return Task.FromResult(default(TResult)); }

            return Task.Run(() => source.ExecuteReaderSingle(commandText, mapper, commandType, parameters), cancellationToken);
        }

        /// <summary>
        /// Executes a reader query against the data base, and returns only one result.
        /// If more than one result was found, throws exception.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The <see cref="IDatabase"/> instance.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandType">The command type.</param>
        /// <param name="mapper">The mapper for result projection.</param>
        /// <param name="parameters">The command parameters.</param>
        /// <returns>A instance of <typeparamref name="TResult"/>.</returns>
        public static TResult ExecuteReaderSingle<TResult>(this IDatabase source, string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            if (source == null) { return default(TResult); }

            return source.ExecuteReader(commandText, mapper, commandType, parameters).SingleOrDefault();
        }

        #endregion Public Static Methods
    }
}