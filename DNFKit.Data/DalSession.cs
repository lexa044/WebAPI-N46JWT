using System.Data.Common;
using System.Data.SqlClient;

using DNFKit.Core;

namespace DNFKit.Data
{
    public sealed class DalSession : IDalSession
    {
        private readonly string _readConnectionString;
        private readonly IUnitOfWork _uom;
        private SqlConnection _connection;

        public DalSession(string readConnectionString, string writeConnectionString)
        {
            _readConnectionString = readConnectionString;
            _uom = new UnitOfWork(writeConnectionString);
        }

        public void Dispose()
        {
            if (null != _connection)
                _connection.Dispose();

            if (null != _uom)
                _uom.Dispose();

            _connection = null;
        }

        public DbConnection GetReadOnlyConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new SqlConnection(_readConnectionString);
            _connection.Open();
            return _connection;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return _uom;
        }
    }
}
