using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountManager.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid ID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public String FirstName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public String MiddleName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public String LastName { get; set; }
        [Column(TypeName = "varchar(200)")]
        public String Gender { get; set; }
        [Column(TypeName = "varchar(200)")]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "Text")]
        public String Address { get; set; }

        public string Role { get; set; }
        public string EmailAddress { get; set; }
        
        public Account Account { get; set; }
        public string Password { get; set; }

        public string BVN { get; set; }
        public DateTime DateCreated { get; set; }
        public string Token { get; set; }
    }
}
