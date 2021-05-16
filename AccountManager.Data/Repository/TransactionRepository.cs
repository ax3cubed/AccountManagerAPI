using AccountManager.Data.Interface;
using AccountManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransaction
    {
        private readonly AccountManagerContext _db;
        public TransactionRepository(AccountManagerContext db) : base(db)
        {
            // Create the database if it doesn't exist
            
            _db = db;
        }
        public async Task<Transaction> GetTransactionByTransactionId(Guid id) => await GetOneBy(transaction => transaction.TransactionID == id);

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountNumber(string accountNumber)
        {
            return await GetBy(transactions => transactions.AccountNumber == accountNumber);
        }
    }
}
