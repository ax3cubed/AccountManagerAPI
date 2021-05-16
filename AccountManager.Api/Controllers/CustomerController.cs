//using AccountManager.Business;
//using AccountManager.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ITeller.Api.Controllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        private readonly CustomerBusiness _customerBusiness;

//        public CustomerController(CustomerBusiness customerBusiness) => 
//            _customerBusiness = customerBusiness;

//        [HttpGet]
//        public async Task<ActionResult> Get()
//        {
//            var customers = await _customerBusiness.GetCustomers();
//            return Ok(customers);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult> Get(Guid id)
//        {
//            var customer = await _customerBusiness.GetCustomerByID(id);
//            return Ok(customer);
//        }

        
        
//        [HttpPost]
//        public async Task<ActionResult> Post(Customer customer)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid entries!");

//            await _customerBusiness.Create(customer);
//            return Ok(customer);
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult> Put([FromBody] Customer customer, Guid id)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest("Invalid entries!");

//            if(customer.ID != id)
//                return BadRequest("Invalid record!");

//            await _customerBusiness.Update(customer);
//            return Ok(customer);
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delate(Guid id)
//        {
//            await _customerBusiness.Delete(id);
//            return Ok("Record deleted successfully");
//        }
//    }
//}
