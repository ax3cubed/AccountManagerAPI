using AccountManager.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly AccountManagerContext _db;
        public UserRepository(AccountManagerContext db) : base(db) 
        {
            // Create the database if it doesn't exist
            db.Database.EnsureCreated();
            _db = db;
        }

        public async Task<User> GetUserByEmail(string EmailAddress) => await GetOneBy(user => user.EmailAddress == EmailAddress);

        public async Task<User> GetUserByID(Guid userID) =>  await GetOneBy(p => p.ID == userID);

    }
}
