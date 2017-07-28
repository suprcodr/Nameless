using System;
using System.Collections.Generic;
using System.Data;

namespace Nameless.Framework.Data.Ado {

    public sealed class TransactionAwareDatabase : IDatabase, IDisposable {

        #region Private Read-Only Fields

        private readonly IDatabase _decorator;

        #endregion Private Read-Only Fields

        #region Private Fields

        private IDbTransaction _transaction;

        private bool _disposed;

        #endregion Private Fields

        #region Public Constructors

        public TransactionAwareDatabase(IDatabase decorator) {
            Prevent.ParameterNull(decorator, nameof(decorator));

            _decorator = decorator;

            Initialize();
        }

        #endregion Public Constructors

        #region Destructor

        /// <summary>
        /// Destructor
        /// </summary>
        ~TransactionAwareDatabase() {
            Dispose(disposing: false);
        }

        #endregion Destructor

        #region Private Methods

        private void Initialize() {
            _transaction = _decorator is IDbConnectionAccessor accessor
                ? accessor.Connection.BeginTransaction()
                : NullDbTransaction.Instance;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> if called from managed code; otherwise <c>false</c>.</param>
        private void Dispose(bool disposing) {
            if (_disposed) { return; }
            if (disposing) {
                try { _transaction.Commit(); } catch { _transaction.Rollback(); throw; } finally { _transaction.Dispose(); }
            }

            // Dispose your unmanaged resources here
            _transaction = null;
            _disposed = true;
        }

        #endregion Private Methods

        #region Private Methods

        private void BlockAccessAfterDisposed() {
            if (_disposed) {
                throw new ObjectDisposedException(nameof(TransactionAwareDatabase));
            }
        }

        #endregion Protected Methods

        #region IDatabase Members

        public int ExecuteNonQuery(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            BlockAccessAfterDisposed();

            return _decorator.ExecuteNonQuery(commandText, commandType, parameters);
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(string commandText, Func<IDataReader, TResult> mapper, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            BlockAccessAfterDisposed();

            return _decorator.ExecuteReader(commandText, mapper, commandType, parameters);
        }

        public TResult ExecuteScalar<TResult>(string commandText, CommandType commandType = CommandType.Text, params Parameter[] parameters) {
            BlockAccessAfterDisposed();

            return _decorator.ExecuteScalar<TResult>(commandText, commandType, parameters);
        }

        #endregion IDatabase Members

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}