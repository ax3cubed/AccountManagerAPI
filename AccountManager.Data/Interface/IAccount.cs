using AccountManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public interface IAccount : IGeneric<Account>
    {
        Task<Account> GetAccountByAccountNumber(string accountNumber);
        Task<List<Transaction>> GetTransactionsByAccountNumber(string accountNumber);
    }
}
