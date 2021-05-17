using AccountManager.Business;
using AccountManager.Helpers;
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
    public class UserController : ControllerBase
    {
        private readonly UserBusiness _userBusiness;
        private readonly JWTService _jwtService;

        public UserController(UserBusiness userBusiness, JWTService jWTService)
        {
            _userBusiness = userBusiness;
            _jwtService = jWTService;
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userBusiness.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var user = await _userBusiness.GetUserByID(id);
            return Ok(user);
        }

        [HttpGet("getByEmail")]
        [Authorize]
        public async Task<ActionResult> GetUserByEmailAddress([FromQuery] string emailAddress)
        {
            var user = await _userBusiness.GetUserByEmail(emailAddress);
            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterVM register)
        {
            var result = await _userBusiness.Register(register);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Login([FromBody] AuthenticateModel model)
        {
            var result = await _userBusiness.Login(model.EmailAddress, model.Password);
            if (result == null)
                return Unauthorized("Email Address or Password is Invalid!");

            var token = _jwtService.GenerateSecurityToken(result);

            Response.Headers.Add("x-accountManager-auth-token", token);
            result.Token = token;
           
            return Ok(new ResponseMessage { Data=result,Code="0", Message = "Login Succesful"});
        }

        [Authorize]
        [HttpGet("login")]
        public async Task<ActionResult> GetLoggedInuser([FromQuery] string token) {
            var result = await _userBusiness.GetLoggedInUser(token);
            if (result == null) return Unauthorized("Token Invalid");
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("savedata")]
        public async Task<ActionResult> SaveUsers() {

            var result = await _userBusiness.CreateData();
            return Ok(result);
        }
    }
}
