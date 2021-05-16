using Microsoft.AspNetCore.Mvc;
using System;
using AccountManager.Models;
using AccountManager.Business;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionManager.Business;
using AccountManager.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace AccountManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController :ControllerBase
    {
        private readonly TransactionBusiness _transactionBusiness;
        private readonly AccountBusiness _accountBusiness;

        public TransactionController(TransactionBusiness transactionBusiness, AccountBusiness accountBusiness)
        {
            this._transactionBusiness = transactionBusiness;
            this._accountBusiness = accountBusiness;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<ActionResult> GetAllTransactions() {
            var  transactions = await _transactionBusiness.GetTransactions();
            return Ok(transactions);
        }

        [HttpGet("{transactionId}")]
        [Authorize]
        public async Task<ActionResult> GetTransactionByTransactionID(Guid transactionId)
        {
            var transaction = await _transactionBusiness.GetTransactionByID(transactionId);
            return Ok(transaction);
        }

        [HttpGet("{accountNumber}")]
        [Authorize]
        public async Task<ActionResult> GetTransactionsByAccountNumber(string accountNumber) {
            var transactions = await _transactionBusiness.GetTransactionsByAccountNumber(accountNumber);
            return Ok(transactions);
                }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Transfer")]
        public async Task<ActionResult> Transfer([FromBody] TransferVM transfer) {

            var response = await _transactionBusiness.Transfer(transfer);
            return Ok(response);
        }

       
    }
}
