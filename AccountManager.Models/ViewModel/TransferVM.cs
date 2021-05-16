using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Models.ViewModel
{
    public class TransferVM
    {
        public string   FromAccount { get; set; }
        public string ToAccount { get; set; }
        public int Amount { get; set; }


    }
}
