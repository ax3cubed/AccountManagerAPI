using AccountManager.Models;
using AccountManager.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public class AccountRepository : GenericRepository<Account>, IAccount
    {
        public AccountRepository(AccountManagerContext db) : base(db) { }

        public async Task<Account> GetAccountByAccountNumber(string accountNumber) => await GetOneBy(account => account.AccountNumber == accountNumber);

        public async Task<List<Transaction>> GetTransactionsByAccountNumber(string accountNumber)
        {
            Account response =  await GetOneBy(account => account.AccountNumber == accountNumber);
            return response.Transactions;
        }

        }
}
