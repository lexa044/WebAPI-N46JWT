using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using DNFKit.Core;

namespace DNFKit.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private readonly string _connectionString;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection GetConnection(bool transactional = false, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return _connection;
        }

        public DbTransaction GetTransaction()
        {
            return _transaction;
        }

        public void CommitChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _connection.Close();
            }
        }

        public void Dispose()
        {
            if (null != _transaction)
                _transaction.Dispose();

            if (null != _connection)
                _connection.Dispose();

            _transaction = null;
            _connection = null;
        }
    }
}
