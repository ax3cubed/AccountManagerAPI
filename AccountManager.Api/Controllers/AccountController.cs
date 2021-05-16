using AccountManager.Business;
using AccountManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountBusiness _accountBusiness;

        public AccountController(AccountBusiness accountBusiness) => 
            _accountBusiness = accountBusiness;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var accounts = await _accountBusiness.GetAccounts();
            return Ok(accounts);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var account = await _accountBusiness.GetAccountByID(id);
            return Ok(account);
        }

        
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> Post(Account account)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            await _accountBusiness.Create(account);
            return Ok(account);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] Account account, Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid entries!");

            if(account.ID != id)
                return BadRequest("Invalid record!");

            await _accountBusiness.Update(account);
            return Ok(account);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _accountBusiness.Delete(id);
            return Ok("Record deleted successfully");
        }
    }
}
