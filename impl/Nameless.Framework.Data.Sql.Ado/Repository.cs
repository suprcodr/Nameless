using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Nameless.Framework.Data.Sql.Ado {

    public sealed class Repository : IRepository, IDbConnectionAccessor, IDisposable {

        #region Private Read-Only Fields

        private readonly ICommandQueryFactory _factory;
        private readonly DatabaseSettings _settings;
        private readonly IDbProvider _provider;

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
        /// <param name="provider">Data base provider instance.</param>
        /// <param name="factory"></param>
        public Repository(ICommandQueryFactory factory, DatabaseSettings settings, IDbProvider provider) {
            Prevent.ParameterNull(settings, nameof(settings));
            Prevent.ParameterNull(factory, nameof(factory));
            Prevent.ParameterNull(provider, nameof(provider));

            _settings = settings;
            _factory = factory;
            _provider = provider;
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
                _connection = _provider.GetFactory().CreateConnection();

                _connection.ConnectionString = _settings.ConnectionString;
                _connection.Open();
            }

            return _connection;
        }

        private DbParameter ConvertParameter(Parameter parameter) {
            var result = _provider.GetFactory().CreateParameter();

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

        public Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.Run(() => {
                var command = _factory.Create<TEntity>().ForDelete(entity);

                ExecuteNonQuery(
                    commandText: command.GetSql(),
                    commandType: command.GetCommandType(),
                    parameters: command.GetParameters());
            });
        }

        public Task<dynamic> ExecuteDirectiveAsync<TDirective>(dynamic parameters, CancellationToken cancellationToken = default(CancellationToken)) where TDirective : IDirective {
            Prevent.ParameterNull(parameters, nameof(parameters));

            if (!typeof(Directive).GetTypeInfo().IsAssignableFrom(typeof(TDirective))) {
                throw new InvalidOperationException($"Directive must inherit from \"{typeof(Directive)}\"");
            }

            var directive = (IDirective)Activator.CreateInstance(typeof(TDirective), new object[] { _connection });

            return directive.ExecuteAsync(parameters);
        }

        public Task<TEntity> FindOneAsync<TEntity>(object id, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.Run(() => {
                var query = _factory.Create<TEntity>().ForFindOneByID(id);

                return ExecuteReader(
                    commandText: query.Sql,
                    commandType: query.CommandType,
                    mapper: query.Mapper,
                    parameters: query.Parameters.ToArray()).SingleOrDefault();
            });
        }

        public Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.Run(() => {
                var query = _factory.Create<TEntity>().ForFindOneByExpression(where);

                return ExecuteReader(
                    commandText: query.Sql,
                    commandType: query.CommandType,
                    mapper: query.Mapper,
                    parameters: query.Parameters.ToArray()).SingleOrDefault();
            });
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class {
            var query = _factory.Create<TEntity>().ForQuery();

            return ExecuteReader(
                commandText: query.Sql,
                commandType: query.CommandType,
                mapper: query.Mapper,
                parameters: query.Parameters.ToArray()).AsQueryable();
        }

        public Task SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class {
            return Task.Run(() => {
                var command = _factory.Create<TEntity>().ForSave(entity);

                ExecuteNonQuery(
                    commandText: command.GetSql(),
                    commandType: command.GetCommandType(),
                    parameters: command.GetParameters());
            });
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