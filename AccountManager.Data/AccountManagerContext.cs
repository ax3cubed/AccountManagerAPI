using AccountManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManager.Data
{
    public class AccountManagerContext : DbContext
    {
        public AccountManagerContext(DbContextOptions<AccountManagerContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        //public virtual DbSet<User> UserVMs { get; set; }
    }
}
