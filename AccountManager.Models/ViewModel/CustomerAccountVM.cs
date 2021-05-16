using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Models
{
    public class CustomerAccountVM
    {
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
