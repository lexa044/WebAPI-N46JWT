using System;
using System.Data.Common;

namespace DNFKit.Core
{
    public interface IDalSession : IDisposable
    {
        DbConnection GetReadOnlyConnection();
        IUnitOfWork GetUnitOfWork();
    }
}
