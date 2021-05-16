using AccountManager.Data;
using AccountManager.Models;
using AccountManager.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Business
{
    public class AccountBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Account>> GetAccounts() => 
            await _unitOfWork.Accounts.GetAll();

        public async Task<Account> GetAccountByID(Guid id) => 
            await _unitOfWork.Accounts.Find(id);

        public async Task<Account> GetAccountByAccountNumber(string accountNumber) => await _unitOfWork.Accounts.GetAccountByAccountNumber(accountNumber);

        public async Task<List<Transaction>> GetTansactionsByAccountNumber(string accountNumber) => await _unitOfWork.Accounts.GetTransactionsByAccountNumber(accountNumber);
        public async Task Create(Account account)
        {
            await _unitOfWork.Accounts.Create(account);
            await _unitOfWork.Commit();
        }


        public async Task Update(Account account)
        {
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var entity= await GetAccountByID(id);
            _unitOfWork.Accounts.Delete(entity);
        }

        
    }
}
