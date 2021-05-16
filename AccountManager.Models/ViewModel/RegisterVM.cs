using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Models
{
    public class RegisterVM
    {
        public string EmailAddress { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal InitialDeposit { get; set; }
    }
}
