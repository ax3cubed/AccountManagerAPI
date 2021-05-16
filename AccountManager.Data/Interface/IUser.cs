using AccountManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public interface IUser : IGeneric<User>
    {
        Task<User> GetUserByEmail(string adminEmailAddress);
        //Task<UserVM> GetUserByEmailAddress(string emailAddress);
    }
}
