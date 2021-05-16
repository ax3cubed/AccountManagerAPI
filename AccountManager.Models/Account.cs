using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManager.Models
{

    [Table("Account")]
    public class Account
    {
        [Key]
        public Guid ID { get; set; }
        public string UserEmailAddress { get; set; }

        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
