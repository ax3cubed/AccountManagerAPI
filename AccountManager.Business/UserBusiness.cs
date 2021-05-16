using AccountManager.Data;
using AccountManager.Models;
using AccountManager.Services;
using AccountManager.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Models.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AccountManager.Business
{
    public class UserBusiness
    {
        private List<User> _users = new List<User>
        {
           
        };
        private readonly IUnitOfWork _unitOfWork;
        private readonly EncryptionService _encryptionService;
         

        
 
        public UserBusiness(IUnitOfWork unitOfWork,EncryptionService encryptionService)
        {

            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
           
           

        }

        public async Task<List<User>> GetUsers() =>
            await _unitOfWork.Users.GetAll();

        public async Task<User> GetUserByID(Guid id) =>
            await _unitOfWork.Users.Find(id);

        public async Task<User> GetUserByEmail(string emailAddress)
        {
            return await _unitOfWork.Users.GetUserByEmail(emailAddress);
        }

 
        public async Task Create(User user)
        {
            //user.BVN = await _bvnService.GetBvnByPhoneNumber(user.PhoneNumber);
            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Commit();
        }

        public async Task Update(User user)
        {
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetUserByID(id);
            _unitOfWork.Users.Delete(entity);
        }

        public async Task<ResponseMessage> Register(RegisterVM register)
        {
            var user = await GetUserByEmail(register.EmailAddress);
            if (user != null)
                return new ResponseMessage
                {
                    Data = null,
                    Message = "Email address already exist!"
                };

            var userId = Guid.NewGuid();
            var password = userId.ToString().Split("-")[0];
            var fullname = $"{register.Surname.ToUpper()} {register.Othername.ToUpper().Substring(0, 1)}. {register.Firstname.ToUpper()}";
            var dateCreated = DateTime.Now;

            var account = new Account
            {
                Balance = register.InitialDeposit,
                AccountNumber = Utilities.GenerateNumber(11),
                Name = register.Surname + " " + register.Firstname + " " + register.Othername,
                Transactions = new List<Transaction>(),
                DateCreated = DateTime.Now,
                UserEmailAddress = register.EmailAddress
            };

            user = new User
            {
                ID = userId,
                EmailAddress = register.EmailAddress,
                Password = _encryptionService.Encrypt(password),
                BVN = Utilities.GenerateNumber(10),
                DateCreated = dateCreated,
                Role = Role.User,
                Account = account
            };


            
             
            try
            {
                await _unitOfWork.Users.Create(user);
                await _unitOfWork.Accounts.Create(account);
                await _unitOfWork.Commit();

                return new ResponseMessage { Data = user.WithoutPassword(), Message = "User registered successfully!" };
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ResponseMessage { Data = null, Message = "Error creating user." + ex.InnerException };
            }

        }

        public async Task<UserSession> Login(string emailAddress, string passowrd)
        {
            var user = await GetUserByEmail(emailAddress);
            if (user == null) return null;


            var isValid = _encryptionService.Validate(passowrd, user.Password);
            if (!isValid) return null;

             
            var userWithoutPassword= user.WithoutPassword();

            return await CreateUserSession(userWithoutPassword);
        }
        private async Task<UserSession> GetUserSession() {

            return null; 
        }

        private async Task<UserSession> CreateUserSession(User user)
        {
            var person = await GetUserByID(user.ID);
            var userSession = new UserSession
            {
                UserID = user.ID,
                EmailAddress = user.EmailAddress,
                FullName = user.FirstName + " " + user.MiddleName + " " + user.LastName
            };

            return userSession;
        }
        public async Task<User> GetLoggedInUser(string token) {
            var userSession = GetUserFromToken(token);
            return await GetUserByEmail(userSession.EmailAddress);
        }

        public UserSession GetUserFromToken(string token)
        {
            
            var userSession = new UserSession();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var subject = tokenHandler.ReadJwtToken(token).Claims.ToList();
                userSession.EmailAddress = subject[1].Value;
                userSession.UserID = new Guid(subject[0].Value);
                userSession.FullName = subject[3].Value;
                return userSession;

            }
            catch (Exception)
            {

                return null;
            }

           
        }
        public async Task<ResponseMessage> Logout(string token) {

            
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var subject = tokenHandler.ReadJwtToken(token);
                
                

            }
            catch (Exception)
            {

                return null;
            }
            return new ResponseMessage();
        }

        public async Task<ResponseMessage> CreateData()
        {
            User user1 = new User { 
                ID = Guid.NewGuid(), 
                FirstName = "Admin", LastName = "User", 
                EmailAddress = "admin@gmail.com", 
                Password = _encryptionService.Encrypt("admin"), 
                Role = Role.Admin, 
                Address="",
                BVN="",
                Gender="Male",
                
                Account= new Account()
            };
            //User user2 = new User
            //{
            //    ID = Guid.NewGuid(),
            //    FirstName = "Normal",
            //    LastName = "User",
            //    EmailAddress = "user@gmail.com",
            //    Password = "user",
            //    Role = Role.User,
            //    Address = "",
            //    BVN = "",
            //    Gender = "Male",
            //    Account ={
            //        AccountNumber= Utilities.GenerateNumber(10),
            //        Balance = 0,
            //        DateCreated= DateTime.Now,
            //        Name = "Normal User ",
            //        UserEmailAddress="snormal@user.com",
            //        //Transactions = new List<Transaction>()
            //    }
            //};
            try
            {
                await _unitOfWork.Users.Create(user1);
                await _unitOfWork.Commit();
                //await _unitOfWork.Users.Create(user2);
                //await _unitOfWork.Accounts.Create(user2.Account);
                return new ResponseMessage { Data = user1.WithoutPassword(), Message = "User registered successfully!" };
            }
            catch (Exception ex)
            {

                _unitOfWork.Rollback();
                return new ResponseMessage { Data = null, Message = "Error creating user." + ex.InnerException };

            }
            
        }
    }

  
}
