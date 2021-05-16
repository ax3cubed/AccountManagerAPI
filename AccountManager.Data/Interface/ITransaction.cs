using AccountManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data.Interface
{
    public interface ITransaction: IGeneric<Transaction>
    {
        Task<IEnumerable<Transaction>> GetTransactionsByAccountNumber(string accountNumber);
        Task<Transaction> GetTransactionByTransactionId(Guid id);
    }
}
