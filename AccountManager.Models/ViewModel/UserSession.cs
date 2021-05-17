using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Models.ViewModel
{
    public class UserSession
    {
        public Guid UserID { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }

        public string Token { get; set; }

    }
}
