using System;
using System.Data;
using System.Data.Common;

namespace DNFKit.Core
{
    public interface IUnitOfWork : IDisposable
    {
        DbConnection GetConnection(bool transactional = false, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        DbTransaction GetTransaction();
        void CommitChanges();
    }
}
