using System;
using System.Collections.Generic;
using System.Text;
using AccountManager.Models;
using System.Threading.Tasks;
using AccountManager.Data;
using System.Linq;
using AccountManager.Models.ViewModel;
using AccountManager.Helpers;

namespace TransactionManager.Business
{
    public class TransactionBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionBusiness(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task<List<Transaction>> GetTransactions() =>
           await _unitOfWork.Transactions.GetAll();

        public async Task<List<Transaction>> GetTransactionsByAccountNumber(string accountNumber) =>
            await Task.FromResult(_unitOfWork.Transactions.GetTransactionsByAccountNumber(accountNumber).Result.ToList());

        public async Task<Transaction> GetTransactionByID(Guid id) =>
            await _unitOfWork.Transactions.Find(id);

        public async Task Create(Transaction Transaction)
        {
            await _unitOfWork.Transactions.Create(Transaction);
            await _unitOfWork.Commit();
        }

        public async Task Update(Transaction Transaction)
        {
            _unitOfWork.Transactions.Update(Transaction);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetTransactionByID(id);
            _unitOfWork.Transactions.Delete(entity);
        }
        public async Task<ResponseMessage> Transfer(TransferVM transfer)
        {
            Account SenderAccount = await _unitOfWork.Accounts.GetAccountByAccountNumber(transfer.FromAccount);
            Account ReceIverAccount = await _unitOfWork.Accounts.GetAccountByAccountNumber(transfer.ToAccount);

            if (SenderAccount == null) return new ResponseMessage { Code = "999", Data = null, Message = "This Account does not exist" };
            if (ReceIverAccount == null) return new ResponseMessage { Code = "999", Data = null, Message = "This Account does not exist" };


            if (this.isTransferPossible(transfer))
            {

                SenderAccount.Balance -= transfer.Amount;
                ReceIverAccount.Balance += transfer.Amount;

                var senderTransaction = new Transaction
                {
                    AccountNumber = transfer.FromAccount,
                    Amount = transfer.Amount,
                    Date = DateTime.Now,
                    ReferenceNumber = Utilities.GenerateNumber(16),
                    Type = TransferType.Debit
                };
                var receiverTransaction = new Transaction
                {
                    AccountNumber = transfer.ToAccount,
                    Amount = transfer.Amount,
                    Date = DateTime.Now,
                    ReferenceNumber = Utilities.GenerateNumber(16),
                    Type = TransferType.Credit
                };
                try
                {
                    _unitOfWork.Accounts.Update(SenderAccount);
                    _unitOfWork.Accounts.Update(ReceIverAccount);
                    await _unitOfWork.Transactions.Create(senderTransaction);
                    await _unitOfWork.Transactions.Create(receiverTransaction);

                    await _unitOfWork.Commit();

                    return new ResponseMessage { Data =senderTransaction , Message = "Transfer successfully!" };
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    return new ResponseMessage { Data = null, Message = "Error Occured in transfer" + ex.InnerException };
                }
            }
            else
            {
                return new ResponseMessage { Code = "333", Data = null, Message = "Insufficient Funds!!" };
            }
        }
        public bool isTransferPossible(TransferVM transfer)
        {

            var SenderAccount = this._unitOfWork.Accounts.GetAccountByAccountNumber(transfer.FromAccount).Result;
            return SenderAccount.Balance > transfer.Amount;
        }

    }
}
