using AccountManager.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
        void Rollback();

        IUser Users { get; }
        IAccount Accounts { get; }
        ITransaction Transactions { get; }
    }
}
