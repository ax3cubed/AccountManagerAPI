using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Models
{
    public class AdminSession
    {
        public Guid AdminID { get; set; }
        public string EmailAddress { get; set; }
        public string Fullname { get; set; }

    }
}
