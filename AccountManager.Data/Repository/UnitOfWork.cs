using AccountManager.Data;
using AccountManager.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountManagerContext _db;
        public UnitOfWork(AccountManagerContext db, IUser User,  IAccount accounts)
        {
            _db = db;
            Users = User;
             
            Accounts = accounts;
           
        }

        public IUser Users { get; }
     
        public IAccount Accounts { get; }

        public ITransaction Transactions { get; }

        public async Task<int> Commit() =>
            await _db.SaveChangesAsync();

        public void Rollback() => Dispose();

        public void Dispose() => 
            _db.DisposeAsync();
    }
}
