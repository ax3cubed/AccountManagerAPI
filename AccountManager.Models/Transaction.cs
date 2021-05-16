using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountManager.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public Guid TransactionID { get; set; }
        public string Type { get; set; } // 0 for debit and 1 for credit
        public double Amount  { get; set; }

        public DateTime Date { get; set; }
        public string AccountNumber { get; set; }

        public string ReferenceNumber { get; set; }
    }
}
