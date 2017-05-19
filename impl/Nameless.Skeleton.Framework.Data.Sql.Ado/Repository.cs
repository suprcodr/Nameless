using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Nameless.Skeleton.Framework.Data.Sql.Ado {

    public sealed class Repository : IRepository, IDbConnectionAccessor, IDisposable {

        #region Private Read-Only Fields

        private readonly DatabaseSettings _settings;
        private readonly IActionInformationExtractorFactory _factory;
        private readonly IDbProvider _dbProvider;

        #endregion Private Read-Only Fields

        #region Private Fields

        private DbConnection _connection;
        private bool _disposed;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Repository"/>.
        /// </summary>
        /// <param name="settings">The configuration section.</param>
        /// <param name="dbProvider">Data base provider instance.</param>
        /// <param name="factory"></param>
        public Repository(DatabaseSettings settings, IActionInformationExtractorFactory factory, IDbProvider dbProvider) {
            Prevent.ParameterNull(settings, nameof(settings));
            Prevent.ParameterNull(factory, nameof(factory));
            Prevent.ParameterNull(dbProvider, nameof(dbProvider));

            _settings = settings;
            _factory = factory;
            _dbProvider = dbProvider;
        }

        #endregion Public Constructors

        #region Destructor

        ~Repository() {
            Dispose(disposing: false);
        }

        #endregion Destructor

        #region Private Methods

        private DbConnection GetConnection() {
            if (_connection == null) {
                _connection = _dbProvider.GetFactory().CreateConnection();

                _connection.ConnectionString = _settings.ConnectionString;
                _connection.Open();
            }

            return _connection;
        }

        private DbParameter ConvertParameter(Parameter parameter) {
            var result = _dbProvider.GetFactory().CreateParameter();

            result.ParameterName = (!parameter.Name.StartsWith("@") ? string.Concat("@", parameter.Name) : parameter.Name);
            result.DbType = parameter.Type;
            result.Direction = parameter.Direction;
            result.Value = (parameter.Value ?? DBNull.Value);

            return result;
        }

        private void PrepareCommand(DbCommand command, string commandText, CommandType commandType, Parameter[] parameters) {
            command.CommandText = commandText;
            command.CommandType = commandType;

            parameters.Each(parameter => command.Parameters.Add(ConvertParameter(parameter)));
        }

        private object Execute(string commandText, CommandType commandType, Parameter[] parameters, bool scalar) {
            using (var command = GetConnection().CreateCommand()) {
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

        private int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            return (int)Execute(commandText, commandType, parameters, false);
        }

        private IEnumerable<TResult> ExecuteReader<TResult>(string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            using (var command = GetConnection().CreateCommand()) {
                PrepareCommand(command, commandText, commandType, parameters);
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        yield return mapper(reader);
                    }
                }
            }
        }

        private object ExecuteScalar(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            return Execute(commandText, commandType, parameters, true);
        }

        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                if (_connection != null) {
                    if (_connection.State == ConnectionState.Open) {
                        _connection.Close();
                    }
                    _connection.Dispose();
                }
            }

            _connection = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region IRepository Members

        public void Delete<TEntity>(TEntity entity) where TEntity : class {
            var actionInformation = _factory.Create<TEntity>().ForSave(entity);

            ExecuteNonQuery(
                commandText: actionInformation.Text,
                commandType: actionInformation.Type,
                parameters: actionInformation.Parameters.ToArray());
        }

        public dynamic ExecuteDirective<TDirective>(dynamic parameters) where TDirective : IDirective {
            Prevent.ParameterNull(parameters, nameof(parameters));

            if (!typeof(Directive).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive)}\"");
            }

            var directive = (IDirective)Activator.CreateInstance(typeof(TDirective), new object[] { _connection });

            return directive.Execute(parameters);
        }

        public TEntity FindOne<TEntity>(object id) where TEntity : class {
            var actionInformation = _factory.Create<TEntity>().ForFindOneByID(id);

            return ExecuteReader(
                commandText: actionInformation.Text,
                commandType: actionInformation.Type,
                mapper: actionInformation.Mapper,
                parameters: actionInformation.Parameters.ToArray()).SingleOrDefault();
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class {
            var actionInformation = _factory.Create<TEntity>().ForFindOneByExpression(where);

            return ExecuteReader(
                commandText: actionInformation.Text,
                commandType: actionInformation.Type,
                mapper: actionInformation.Mapper,
                parameters: actionInformation.Parameters.ToArray()).SingleOrDefault();
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            var actionInformation = _factory.Create<TEntity>().ForQuery();

            return ExecuteReader(
                commandText: actionInformation.Text,
                commandType: actionInformation.Type,
                mapper: actionInformation.Mapper,
                parameters: actionInformation.Parameters.ToArray()).AsQueryable();
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class {
            var actionInformation = _factory.Create<TEntity>().ForSave(entity);

            ExecuteNonQuery(
                commandText: actionInformation.Text,
                commandType: actionInformation.Type,
                parameters: actionInformation.Parameters.ToArray());
        }

        #endregion IRepository Members

        #region IDbConnectionAccessor Members

        public IDbConnection Connection {
            get { return _connection; }
        }

        #endregion IDbConnectionAccessor Members

        #region IDisposable Members

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}