using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Nameless.Framework.Data.Ado {

    /// <summary>
    /// Default implementation of <see cref="IDatabase"/>.
    /// </summary>
    public sealed class Database : IDatabase, IDbConnectionAccessor, IDisposable {

        #region Private Read-Only Fields

        private readonly DatabaseSettings _settings;
        private readonly IDbConnectionProvider _provider;

        #endregion Private Read-Only Fields

        #region Private Fields

        private IDbConnection _writingConnection;
        private IDbConnection _readingConnection;

        private bool _disposed;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Database"/>.
        /// </summary>
        /// <param name="settings">The database settings.</param>
        /// <param name="provider">The provider.</param>
        public Database(DatabaseSettings settings, IDbConnectionProvider provider) {
            Prevent.ParameterNull(settings, nameof(settings));
            Prevent.ParameterNull(provider, nameof(provider));

            _settings = settings;
            _provider = provider;

            Initialize();
        }

        #endregion Public Constructors

        #region Destructor

        /// <summary>
        /// Destructor
        /// </summary>
        ~Database() {
            Dispose(false);
        }

        #endregion Destructor

        #region Private Methods

        private void Initialize() {
            var read = _settings.ConnectionStrings.SingleOrDefault(_ => string.Equals(_.Name, "read", StringComparison.CurrentCultureIgnoreCase));
            var write = _settings.ConnectionStrings.SingleOrDefault(_ => string.Equals(_.Name, "write", StringComparison.CurrentCultureIgnoreCase));
            var readWrite = _settings.ConnectionStrings.SingleOrDefault(_ => string.Equals(_.Name, "read-write", StringComparison.CurrentCultureIgnoreCase));

            if (readWrite == null) {
                if (write == null) { throw new DatabaseConnectionMissingException(nameof(write)); }
                if (read == null) { throw new DatabaseConnectionMissingException(nameof(read)); }

                _writingConnection = _provider.CreateConnection(write.ProviderName, write.Value);
                _readingConnection = _provider.CreateConnection(read.ProviderName, read.Value);
            } else {
                _writingConnection = _readingConnection = _provider.CreateConnection(readWrite.ProviderName, readWrite.Value);
            }
        }

        private IDbDataParameter ConvertParameter(IDbCommand command, Parameter parameter) {
            var result = command.CreateParameter();
            result.ParameterName = (!parameter.Name.StartsWith("@") ? string.Concat("@", parameter.Name) : parameter.Name);
            result.DbType = parameter.Type;
            result.Direction = parameter.Direction;
            result.Value = (parameter.Value ?? DBNull.Value);
            return result;
        }

        private void EnsureAccessBlockedAfterDispose() {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        private void TerminateDbConnection() {
            try { _writingConnection.Close(); _writingConnection.Dispose(); } catch { throw; }
            try { _readingConnection.Close(); _readingConnection.Dispose(); } catch { throw; }
        }

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                TerminateDbConnection();
            }

            _writingConnection = null;
            _readingConnection = null;
            _disposed = true;
        }

        private void PrepareCommand(IDbCommand command, string commandText, CommandType commandType, Parameter[] parameters) {
            command.CommandText = commandText;
            command.CommandType = commandType;

            parameters.Each(parameter => command.Parameters.Add(ConvertParameter(command, parameter)));
        }

        private object Execute(string commandText, CommandType commandType, Parameter[] parameters, bool scalar) {
            using (var command = _writingConnection.CreateCommand()) {
                PrepareCommand(command, commandText, commandType, parameters);

                var result = scalar
                    ? command.ExecuteScalar()
                    : command.ExecuteNonQuery();

                command.Parameters.OfType<DbParameter>()
                    .Where(dbParameter => dbParameter.Direction != ParameterDirection.Input)
                    .Each(dbParameter => {
                        parameters
                            .Single(parameter =>
                                parameter.Name == dbParameter.ParameterName &&
                                parameter.Direction == dbParameter.Direction)
                            .Value = dbParameter.Value;
                    });

                return result;
            }
        }

        #endregion Private Methods

        #region IDatabase Members

        /// <inheritdoc />
        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            EnsureAccessBlockedAfterDispose();

            return (int)Execute(commandText, commandType, parameters, scalar: false);
        }

        /// <inheritdoc />
        public IEnumerable<TResult> ExecuteReader<TResult>(string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            EnsureAccessBlockedAfterDispose();

            using (var command = _readingConnection.CreateCommand()) {
                PrepareCommand(command, commandText, commandType, parameters);

                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        yield return mapper(reader);
                    }
                }
            }
        }

        /// <inheritdoc />
        public TResult ExecuteScalar<TResult>(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            EnsureAccessBlockedAfterDispose();

            return (TResult)Execute(commandText, commandType, parameters, scalar: true);
        }

        #endregion IDatabase Members

        #region IDbConnectionAccessor Members

        /// <summary>
        /// Gets the database connection (write).
        /// </summary>
        public IDbConnection Connection {
            get { return _writingConnection; }
        }

        #endregion IDbConnectionAccessor Members

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose() {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        #endregion IDisposable Members
    }
}